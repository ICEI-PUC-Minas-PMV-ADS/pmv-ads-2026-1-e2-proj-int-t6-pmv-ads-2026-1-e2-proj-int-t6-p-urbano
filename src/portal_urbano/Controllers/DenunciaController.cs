namespace ProjetoUrbano.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjetoUrbano.Data;

    [ApiController]
    [Route("api/[controller]")]
    public class DenunciaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Supabase.Client _supabaseClient;
        private readonly IConfiguration _configuration;

        public DenunciaController(AppDbContext context, Supabase.Client supabaseClient, IConfiguration configuration)
        {
            _context = context;
            _supabaseClient = supabaseClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarioIdAtual = int.TryParse(HttpContext.Session.GetString("UsuarioId"), out var uid) ? uid : 0;

            var denuncias = await _context.Denuncias
                .Include(d => d.Usuario)
                .Include(d => d.Categoria)
                .Include(d => d.Likes)
                .Include(d => d.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .OrderByDescending(d => d.CriadoEm)
                .Select(d => new {
                    d.IdDenuncia,
                    d.Titulo,
                    d.Descricao,
                    Categoria = d.Categoria != null ? d.Categoria.Nome : "Geral",
                    d.Latitude,
                    d.Longitude,
                    Endereco = $"{d.Rua}, {d.Bairro} - {d.Cidade}/{d.Uf}", 
                    d.CriadoEm,
                    d.ImagemUrl,
                    NomeUsuario = d.StatusAnonimo == 1 ? "Usuário Anônimo" : (d.Usuario != null ? d.Usuario.Nome : "Desconhecido"),
                    TotalLikes = d.Likes.Count,
                    TotalComentarios = d.Comentarios.Count,
                    LikedByUser = d.Likes.Any(l => l.UsuarioId == usuarioIdAtual),
                    Comentarios = d.Comentarios.OrderBy(c => c.CriadoEm).Select(c => new {
                        c.IdComentario,
                        c.Texto,
                        c.CriadoEm,
                        NomeUsuario = c.Usuario != null ? c.Usuario.Nome : "Anônimo",
                        IsOwner = c.IdUsuario == usuarioIdAtual // se pode excluir
                    })
                })
                .ToListAsync();

            return Ok(denuncias);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromForm] ProjetoUrbano.Models.Denuncia novaDenuncia, 
            [FromForm] IFormFile[] imagens,
            [FromForm] string? anonimo,
            [FromForm] string? lat_str,
            [FromForm] string? lon_str)
        {
            try
            {
                // Limpa o que o model binder possa ter tentado fazer errado
                novaDenuncia.Latitude = 0;
                novaDenuncia.Longitude = 0;

                // Tenta converter coordenadas manualmente usando InvariantCulture (força o ponto como separador)
                if (!string.IsNullOrEmpty(lat_str))
                {
                    decimal.TryParse(lat_str.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal lat);
                    novaDenuncia.Latitude = lat;
                }
                if (!string.IsNullOrEmpty(lon_str))
                {
                    decimal.TryParse(lon_str.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal lon);
                    novaDenuncia.Longitude = lon;
                }

                // Dados básicos
                novaDenuncia.CriadoEm = DateTime.UtcNow;
                novaDenuncia.Status = "Aberta";
                
                // Tratar usuário anônimo
                novaDenuncia.StatusAnonimo = (anonimo == "on") ? 1 : 0;

                // Associa a denúncia ao usuário logado (MOCK: pegando o primeiro usuário do banco, ou ID = 1)
                var usuario = await _context.Usuarios.FirstOrDefaultAsync();
                if (usuario == null)
                {
                    usuario = new ProjetoUrbano.Models.Usuario { Nome = "Usuário Real", Email = "real@portal.com", SenhaHash = "123", CriadoEm = DateTime.UtcNow };
                    _context.Usuarios.Add(usuario);
                    await _context.SaveChangesAsync();
                }
                novaDenuncia.IdUsuario = usuario.IdUsuario;

                // Garantir que a categoria enviada pelo formulário exista no banco
                if (novaDenuncia.IdCategoria == 0) novaDenuncia.IdCategoria = 1;

                // 1. Salva no SQL primeiro para gerar o ID
                _context.Denuncias.Add(novaDenuncia);
                await _context.SaveChangesAsync();

                // 2. Fazer upload para o Supabase se houver imagens
                if (imagens != null && imagens.Length > 0)
                {
                    var urls = new List<string>();
                    var bucketName = _configuration["Supabase:Bucket"] ?? "imagens_denuncias";

                    for (int i = 0; i < imagens.Length; i++)
                    {
                        var imagem = imagens[i];
                        if (imagem.Length > 0)
                        {
                            using var memoryStream = new MemoryStream();
                            await imagem.CopyToAsync(memoryStream);
                            var bytes = memoryStream.ToArray();

                            // Renomear: denuncia_{Id}_{Indice}.{extensão}
                            var extensao = Path.GetExtension(imagem.FileName);
                            var nomeArquivo = $"denuncia_{novaDenuncia.IdDenuncia}_{i}{extensao}";

                            // Upload para o Supabase
                            await _supabaseClient.Storage
                                .From(bucketName)
                                .Upload(bytes, nomeArquivo, new Supabase.Storage.FileOptions { CacheControl = "3600", Upsert = true });

                            // Pega a URL pública gerada
                            var urlPublica = _supabaseClient.Storage.From(bucketName).GetPublicUrl(nomeArquivo);
                            urls.Add(urlPublica);
                        }
                    }

                    // 3. Atualiza a tabela com a URL (separado por vírgula se for mais de uma)
                    novaDenuncia.ImagemUrl = string.Join(",", urls);
                    await _context.SaveChangesAsync();
                }

                // Redireciona de volta pro Feed ou retorna sucesso
                return Redirect("/Home/Feed");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar a denúncia: {ex.Message}");
            }
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> ToggleLike(int id)
        {
            // Pega o usuário logado (mock para "Usuário Anônimo" ou id 1)
            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            if (usuario == null) return Unauthorized();

            var like = await _context.Gostei.FirstOrDefaultAsync(g => g.DenunciaId == id && g.UsuarioId == usuario.IdUsuario);

            if (like != null)
            {
                // Já curtiu, então exclui (unlike)
                _context.Gostei.Remove(like);
                await _context.SaveChangesAsync();
                return Ok(new { liked = false });
            }
            else
            {
                // Adiciona o like
                _context.Gostei.Add(new ProjetoUrbano.Models.Gostei { DenunciaId = id, UsuarioId = usuario.IdUsuario });
                await _context.SaveChangesAsync();
                return Ok(new { liked = true });
            }
        }

        [HttpPost("{id}/comentario")]
        public async Task<IActionResult> AddComentario(int id, [FromForm] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return BadRequest("O comentário não pode ser vazio.");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync();
            if (usuario == null) return Unauthorized();

            var comentario = new ProjetoUrbano.Models.Comentario
            {
                IdDenuncia = id,
                IdUsuario = usuario.IdUsuario,
                Texto = texto,
                CriadoEm = DateTime.UtcNow
            };

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return Ok(new { 
                id = comentario.IdComentario, 
                texto = comentario.Texto, 
                usuario = usuario.Nome,
                criadoEm = comentario.CriadoEm
            });
        }

        [HttpDelete("comentario/{idComentario}")]
        public async Task<IActionResult> DeleteComentario(int idComentario)
        {
            var comentario = await _context.Comentarios.FindAsync(idComentario);
            if (comentario == null) return NotFound();

            // Num sistema real, verificar se o usuário logado é o dono do comentário!
            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new {
                    c.IdCategoria,
                    c.Nome,
                    c.Descricao,
                    c.Icone
                })
                .ToListAsync();

            return Ok(categorias);
        }
    }
}
