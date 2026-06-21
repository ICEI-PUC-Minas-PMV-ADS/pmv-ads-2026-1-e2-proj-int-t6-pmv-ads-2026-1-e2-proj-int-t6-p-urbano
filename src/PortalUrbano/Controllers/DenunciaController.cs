namespace portal_urbano.Controllers
{
    using System.Globalization;
    using System.Security.Claims;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using portal_urbano.Data;
    using portal_urbano.Models;
    using portal_urbano.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class DenunciaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Supabase.Client _supabaseClient;
        private readonly IConfiguration _configuration;
        private readonly GeminiModeracaoService _geminiModeracao;
        private const int MaxAvisosParaBanimento = 3;
        private const int TotalReportesParaListaNegra = 5;
        private const int TotalReportesParaRemocao = 10;
        private const string StatusListaNegra = "Lista Negra";
        private const string StatusRemovida = "Removida";
        private const long MaxImageBytes = 5 * 1024 * 1024;
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

        private static readonly string[] TermosOfensivos = new[]
        {
            "idiota", "imbecil", "otario", "otaria", "burro", "burra", "bosta", "merda", "porra", "caralho",
            "fdp", "foda-se", "fodase", "vtnc", "vai tomar no cu", "arrombado", "desgracado", "desgracada",
            "corno", "puta", "puto", "vagabundo", "vagabunda", "lixo", "babaca", "trouxa", "canalha",
            "safado", "safada", "filho da puta", "imbecis", "retardado", "retardada"
        };

        private static readonly string[] TermosPoluicao = new[]
        {
            "spam", "cassino", "apostas", "pornografia", "porno", "xxx", "ganhe dinheiro", "renda extra",
            "clique aqui", "compre agora", "http://", "https://", "www.", "bit.ly", "tigrinho",
            "fortune tiger", "1xbet", "blaze", "betano", "ganho facil", "dinheiro facil", "sorteio",
            "gratis", "promocao", "desconto", "comprar seguidores", "pix gratis", "urubu do pix", "renda passiva"
        };

        private static readonly string[] TermosViolenciaAmeaca = new[]
        {
            "matar", "morrer", "assassinar", "agredir", "linchar", "bater", "surra", "espancar",
            "tiro", "arma", "facada", "terrorista", "atentado", "morte"
        };

        public DenunciaController(
            AppDbContext context,
            Supabase.Client supabaseClient,
            IConfiguration configuration,
            GeminiModeracaoService geminiModeracao)
        {
            _context = context;
            _supabaseClient = supabaseClient;
            _configuration = configuration;
            _geminiModeracao = geminiModeracao;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var usuarioIdAtual = ObterUsuarioIdAtual() ?? 0;

            var denunciasBase = await _context.Denuncias
                .AsNoTracking()
                .AsSplitQuery()
                .Include(d => d.Usuario)
                .Include(d => d.Categoria)
                .Include(d => d.Likes)
                .Include(d => d.Reportes)
                .Include(d => d.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .Where(d => d.Status != StatusRemovida)
                .ToListAsync();

            var denuncias = denunciasBase
                .OrderByDescending(d => d.Likes.Count + d.Comentarios.Count)
                .ThenByDescending(d => d.Comentarios.Count)
                .ThenByDescending(d => d.Likes.Count)
                .ThenByDescending(d => d.CriadoEm)
                .Select(d => new
                {
                    d.IdDenuncia,
                    d.Titulo,
                    d.Descricao,
                    d.Status,
                    Categoria = d.Categoria != null ? d.Categoria.Nome : "Geral",
                    d.Latitude,
                    d.Longitude,
                    Endereco = $"{d.Rua}, {d.Bairro} - {d.Cidade}/{d.Uf}",
                    d.CriadoEm,
                    d.ImagemUrl,
                    NomeUsuario = d.StatusAnonimo == 1 ? "Usuário Anônimo" : (d.Usuario != null ? d.Usuario.Nome : "Desconhecido"),
                    TotalLikes = d.Likes.Count,
                    TotalComentarios = d.Comentarios.Count,
                    TotalInteracoes = d.Likes.Count + d.Comentarios.Count,
                    TotalReportes = d.Reportes.Select(r => r.IdUsuario).Distinct().Count(),
                    LikedByUser = usuarioIdAtual > 0 && d.Likes.Any(l => l.UsuarioId == usuarioIdAtual),
                    Comentarios = d.Comentarios.OrderBy(c => c.CriadoEm).Select(c => new
                    {
                        c.IdComentario,
                        c.Texto,
                        c.CriadoEm,
                        NomeUsuario = c.Usuario != null ? c.Usuario.Nome : "Anônimo",
                        IsOwner = usuarioIdAtual > 0 && c.IdUsuario == usuarioIdAtual
                    }).ToList()
                })
                .ToList();

            return Ok(denuncias);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(
            [FromForm] Denuncia novaDenuncia,
            [FromForm] IFormFile[] imagens,
            [FromForm] string? anonimo,
            [FromForm] string? lat_str,
            [FromForm] string? lon_str)
        {
            try
            {
                var usuario = await ObterUsuarioAtualAsync();
                if (usuario == null)
                    return Redirect("/Account/Login");

                if (usuario.Banido)
                    return RedirecionarParaAvisoModeracao(usuario);

                if (string.IsNullOrWhiteSpace(novaDenuncia.Titulo) || string.IsNullOrWhiteSpace(novaDenuncia.Descricao))
                    return BadRequest("Título e descrição são obrigatórios.");

                if (await ConteudoViolaModeracaoAsync(novaDenuncia.Titulo, novaDenuncia.Descricao))
                {
                    usuario.Avisos++;
                    if (usuario.Avisos >= MaxAvisosParaBanimento)
                        usuario.Banido = true;

                    await _context.SaveChangesAsync();
                    return RedirecionarParaAvisoModeracao(usuario);
                }

                novaDenuncia.Latitude = 0;
                novaDenuncia.Longitude = 0;

                if (!string.IsNullOrEmpty(lat_str))
                {
                    decimal.TryParse(lat_str.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var lat);
                    novaDenuncia.Latitude = lat;
                }

                if (!string.IsNullOrEmpty(lon_str))
                {
                    decimal.TryParse(lon_str.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var lon);
                    novaDenuncia.Longitude = lon;
                }

                novaDenuncia.CriadoEm = DateTime.UtcNow;
                novaDenuncia.Status = "Aberta";
                novaDenuncia.StatusAnonimo = anonimo == "on" ? 1 : 0;
                novaDenuncia.IdUsuario = usuario.IdUsuario;

                if (novaDenuncia.IdCategoria == 0)
                    novaDenuncia.IdCategoria = 1;

                _context.Denuncias.Add(novaDenuncia);
                await _context.SaveChangesAsync();

                if (imagens != null && imagens.Length > 0)
                {
                    var urls = new List<string>();
                    var bucketName = _configuration["Supabase:Bucket"] ?? "imagens_denuncias";

                    for (var i = 0; i < imagens.Length; i++)
                    {
                        var imagem = imagens[i];
                        if (imagem.Length == 0)
                            continue;

                        if (imagem.Length > MaxImageBytes)
                            return BadRequest("Cada imagem deve ter no máximo 5 MB.");

                        var extensao = Path.GetExtension(imagem.FileName).ToLowerInvariant();
                        if (!AllowedImageExtensions.Contains(extensao))
                            return BadRequest("Formato de imagem não permitido. Use JPG, PNG ou WEBP.");

                        using var memoryStream = new MemoryStream();
                        await imagem.CopyToAsync(memoryStream);
                        var bytes = memoryStream.ToArray();

                        var nomeArquivo = $"denuncia_{novaDenuncia.IdDenuncia}_{i}_{Guid.NewGuid():N}{extensao}";

                        await _supabaseClient.Storage
                            .From(bucketName)
                            .Upload(bytes, nomeArquivo, new Supabase.Storage.FileOptions { CacheControl = "3600", Upsert = true });

                        var urlPublica = _supabaseClient.Storage.From(bucketName).GetPublicUrl(nomeArquivo);
                        urls.Add(urlPublica);
                    }

                    if (urls.Count > 0)
                    {
                        novaDenuncia.ImagemUrl = string.Join(",", urls);
                        await _context.SaveChangesAsync();
                    }
                }

                return Redirect("/Home/Feed");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao salvar a denúncia. Tente novamente.");
            }
        }

        [HttpPost("{id}/reportar")]
        [Authorize]
        public async Task<IActionResult> ReportarDenuncia(int id, [FromForm] string? motivo, [FromForm] string? detalhes)
        {
            var usuario = await ObterUsuarioAtualAsync();
            if (usuario == null) return Unauthorized();
            if (usuario.Banido) return StatusCode(StatusCodes.Status403Forbidden, new { message = "Usuário banido do sistema." });

            if (string.IsNullOrWhiteSpace(motivo))
                return BadRequest(new { message = "Informe o motivo da denúncia." });

            motivo = motivo.Trim();
            if (motivo.Length > 500)
                motivo = motivo[..500];

            string? detalhesNormalizado = null;
            if (!string.IsNullOrWhiteSpace(detalhes))
            {
                detalhesNormalizado = detalhes.Trim();
                if (detalhesNormalizado.Length > 1000)
                    detalhesNormalizado = detalhesNormalizado[..1000];
            }

            var denuncia = await _context.Denuncias
                .Include(d => d.Reportes)
                .FirstOrDefaultAsync(d => d.IdDenuncia == id);

            if (denuncia == null) return NotFound(new { message = "Denúncia não encontrada." });
            if (denuncia.Status == StatusRemovida) return BadRequest(new { message = "Esta denúncia já foi removida." });

            var usuarioJaReportou = await _context.Reportes
                .AnyAsync(r => r.IdDenuncia == id && r.IdUsuario == usuario.IdUsuario);

            if (usuarioJaReportou)
            {
                var totalAtual = await ContarReportesUnicosAsync(id);
                return Conflict(new
                {
                    message = "Você já denunciou esta postagem.",
                    totalReportes = totalAtual,
                    status = denuncia.Status
                });
            }

            _context.Reportes.Add(new Reporte
            {
                IdDenuncia = id,
                IdUsuario = usuario.IdUsuario,
                Motivo = motivo,
                Detalhes = detalhesNormalizado,
                CriadoEm = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            var totalReportes = await ContarReportesUnicosAsync(id);

            if (totalReportes >= TotalReportesParaRemocao)
                denuncia.Status = StatusRemovida;
            else if (totalReportes >= TotalReportesParaListaNegra)
                denuncia.Status = StatusListaNegra;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = denuncia.Status == StatusRemovida
                    ? "Denúncia removida automaticamente após 10 reportes."
                    : "Denúncia registrada com sucesso.",
                totalReportes,
                status = denuncia.Status,
                removida = denuncia.Status == StatusRemovida
            });
        }

        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var usuario = await ObterUsuarioAtualAsync();
            if (usuario == null) return Unauthorized();
            if (usuario.Banido) return StatusCode(StatusCodes.Status403Forbidden, new { message = "Usuário banido do sistema." });

            var like = await _context.Gostei.FirstOrDefaultAsync(g => g.DenunciaId == id && g.UsuarioId == usuario.IdUsuario);

            if (like != null)
            {
                _context.Gostei.Remove(like);
                await _context.SaveChangesAsync();
                return Ok(new { liked = false });
            }

            _context.Gostei.Add(new Gostei { DenunciaId = id, UsuarioId = usuario.IdUsuario });
            await _context.SaveChangesAsync();
            return Ok(new { liked = true });
        }

        [HttpPost("{id}/comentario")]
        [Authorize]
        public async Task<IActionResult> AddComentario(int id, [FromForm] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return BadRequest(new { message = "O comentário não pode ser vazio." });

            texto = texto.Trim();
            if (texto.Length > 1000)
                texto = texto[..1000];

            var usuario = await ObterUsuarioAtualAsync();
            if (usuario == null) return Unauthorized();
            if (usuario.Banido) return StatusCode(StatusCodes.Status403Forbidden, new { message = "Usuário banido do sistema." });

            var denunciaExiste = await _context.Denuncias.AnyAsync(d => d.IdDenuncia == id && d.Status != StatusRemovida);
            if (!denunciaExiste)
                return NotFound(new { message = "Denúncia não encontrada." });

            var comentario = new Comentario
            {
                IdDenuncia = id,
                IdUsuario = usuario.IdUsuario,
                Texto = texto,
                CriadoEm = DateTime.UtcNow
            };

            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = comentario.IdComentario,
                texto = comentario.Texto,
                usuario = usuario.Nome,
                criadoEm = comentario.CriadoEm
            });
        }

        [HttpDelete("comentario/{idComentario}")]
        [Authorize]
        public async Task<IActionResult> DeleteComentario(int idComentario)
        {
            var usuario = await ObterUsuarioAtualAsync();
            if (usuario == null) return Unauthorized();

            var comentario = await _context.Comentarios.FindAsync(idComentario);
            if (comentario == null) return NotFound();

            if (comentario.IdUsuario != usuario.IdUsuario)
                return Forbid();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("categorias")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new
                {
                    c.IdCategoria,
                    c.Nome,
                    c.Descricao,
                    c.Icone
                })
                .ToListAsync();

            return Ok(categorias);
        }

        private int? ObterUsuarioIdAtual()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(claim, out var id) ? id : null;
        }

        private async Task<Usuario?> ObterUsuarioAtualAsync()
        {
            var id = ObterUsuarioIdAtual();
            if (id == null) return null;
            return await _context.Usuarios.FindAsync(id.Value);
        }

        private Task<int> ContarReportesUnicosAsync(int idDenuncia)
        {
            return _context.Reportes
                .Where(r => r.IdDenuncia == idDenuncia)
                .Select(r => r.IdUsuario)
                .Distinct()
                .CountAsync();
        }

        private async Task<bool> ConteudoViolaModeracaoAsync(string? titulo, string? descricao)
        {
            var resultadoIa = await _geminiModeracao.AnalisarTituloDescricaoAsync(titulo, descricao);
            if (resultadoIa != null)
                return resultadoIa.ViolaRegras;

            return ConteudoViolaModeracaoLocal(titulo, descricao);
        }

        private static bool ConteudoViolaModeracaoLocal(string? titulo, string? descricao)
        {
            var conteudo = NormalizarTexto($"{titulo} {descricao}");
            if (string.IsNullOrWhiteSpace(conteudo))
                return false;

            var possuiOfensa = TermosOfensivos.Any(termo => ContemTermoInteiro(conteudo, termo));
            var possuiPoluicao = TermosPoluicao.Any(termo => conteudo.Contains(NormalizarTexto(termo), StringComparison.Ordinal));
            var possuiViolencia = TermosViolenciaAmeaca.Any(termo => ContemTermoInteiro(conteudo, termo));

            return possuiOfensa || possuiPoluicao || possuiViolencia;
        }

        private static bool ContemTermoInteiro(string conteudoNormalizado, string termo)
        {
            var termoNormalizado = Regex.Escape(NormalizarTexto(termo)).Replace("\\ ", "\\s+");
            var padrao = $@"(^|[^a-z0-9]){termoNormalizado}([^a-z0-9]|$)";
            return Regex.IsMatch(conteudoNormalizado, padrao, RegexOptions.CultureInvariant);
        }

        private static string NormalizarTexto(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            var textoNormalizado = texto.ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var resultado = new StringBuilder(textoNormalizado.Length);

            foreach (var caractere in textoNormalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caractere) == UnicodeCategory.NonSpacingMark)
                    continue;

                var caractereNormalizado = caractere switch
                {
                    '@' => 'a',
                    '4' => 'a',
                    '0' => 'o',
                    '1' => 'i',
                    '3' => 'e',
                    '$' => 's',
                    _ => caractere
                };

                resultado.Append(caractereNormalizado);
            }

            return resultado.ToString().Normalize(NormalizationForm.FormC);
        }

        private RedirectResult RedirecionarParaAvisoModeracao(Usuario usuario)
        {
            var status = usuario.Banido ? "banido" : "aviso";
            return Redirect($"/Home/CriarDenuncia?moderacao={status}&avisos={usuario.Avisos}");
        }
    }
}
