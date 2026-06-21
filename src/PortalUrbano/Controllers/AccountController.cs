using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal_urbano.Data;
using portal_urbano.Models;
using portal_urbano.Services.Email;

namespace portal_urbano.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AppDbContext context, ILogger<AccountController> logger, IEmailService emailService)
        {
            _context = context;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.PasswordResetExpires > DateTime.UtcNow);
            if (usuario == null)
                return RedirectToAction("ForgotPasswordConfirmation");

            return View(model: token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string token, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(novaSenha))
            {
                ModelState.AddModelError("", "Dados inválidos.");
                return View(model: token);
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.PasswordResetExpires > DateTime.UtcNow);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Token inválido ou expirado.");
                return View(model: token);
            }

            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            usuario.PasswordResetToken = null;
            usuario.PasswordResetExpires = null;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string nome, string email, string senha, string cidade, string? bairro, string uf)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(uf))
            {
                ModelState.AddModelError("", "Preencha nome, e-mail, senha, cidade e estado.");
                return View();
            }

            if (senha.Length < 6)
            {
                ModelState.AddModelError("", "A senha deve ter pelo menos 6 caracteres.");
                return View();
            }

            uf = uf.Trim().ToUpperInvariant();
            if (uf.Length != 2)
            {
                ModelState.AddModelError("", "Informe a sigla do estado (ex: MG).");
                return View();
            }

            var exists = await _context.Usuarios.AnyAsync(u => u.Email == email);
            if (exists)
            {
                ModelState.AddModelError("", "Email já cadastrado.");
                return View();
            }

            var usuario = new Usuario
            {
                Nome = nome.Trim(),
                Email = email.Trim(),
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
                Cidade = cidade.Trim(),
                Bairro = string.IsNullOrWhiteSpace(bairro) ? null : bairro.Trim(),
                Uf = uf,
                CriadoEm = DateTime.UtcNow
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            await SignInUsuario(usuario);

            return RedirectToAction("Feed", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Preencha o campo de email.");
                return View();
            }

            // Para evitar enumeração de usuários, sempre redirecionamos para a página de confirmação.
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario != null)
            {
                var token = GeneratePasswordResetToken();
                usuario.PasswordResetToken = token;
                usuario.PasswordResetExpires = DateTime.UtcNow.AddHours(2);
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                try
                {
                    var resetUrl = Url.Action("ResetPassword", "Account", new { token }, Request.Scheme)!;
                    await _emailService.SendPasswordResetAsync(usuario.Email, usuario.Nome, resetUrl);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Falha ao enviar e-mail de redefinição para {Email}", usuario.Email);
                }
            }

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ModelState.AddModelError("", "Preencha todos os campos.");
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            {
                ModelState.AddModelError("", "Credenciais inválidas.");
                return View();
            }

            if (usuario.Banido)
            {
                ModelState.AddModelError("", "Sua conta está suspensa por violação das regras de uso.");
                return View();
            }

            await SignInUsuario(usuario);

            return RedirectToAction("Feed", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (User.Identity?.IsAuthenticated != true)
                return RedirectToAction("Login");

            var usuario = await ObterUsuarioLogadoAsync();
            if (usuario == null)
                return RedirectToAction("Login");

            var minhasDenuncias = await _context.Denuncias
                .AsNoTracking()
                .Include(d => d.Categoria)
                .Include(d => d.Likes)
                .Include(d => d.Comentarios)
                .Where(d => d.IdUsuario == usuario.IdUsuario)
                .OrderByDescending(d => d.CriadoEm)
                .Select(d => new PerfilDenunciaItem
                {
                    IdDenuncia = d.IdDenuncia,
                    Titulo = d.Titulo,
                    Descricao = d.Descricao,
                    Status = d.Status,
                    Categoria = d.Categoria != null ? d.Categoria.Nome : "Geral",
                    CriadoEm = d.CriadoEm,
                    TotalLikes = d.Likes.Count,
                    TotalComentarios = d.Comentarios.Count,
                    ImagemUrl = d.ImagemUrl,
                    Endereco = $"{d.Rua}, {d.Bairro} - {d.Cidade}/{d.Uf}".Trim(' ', ',', '-', '/'),
                    Anonima = d.StatusAnonimo == 1
                })
                .ToListAsync();

            ViewBag.DenunciasCount = minhasDenuncias.Count;
            ViewBag.MinhasDenuncias = minhasDenuncias;

            return View(usuario);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirDenuncia(int id)
        {
            var usuario = await ObterUsuarioLogadoAsync();
            if (usuario == null)
                return RedirectToAction("Login");

            var denuncia = await _context.Denuncias.FirstOrDefaultAsync(d => d.IdDenuncia == id && d.IdUsuario == usuario.IdUsuario);
            if (denuncia == null)
            {
                TempData["ErroDenuncia"] = "Denúncia não encontrada ou você não tem permissão para excluí-la.";
                return RedirectToAction(nameof(Profile));
            }

            _context.Denuncias.Remove(denuncia);
            await _context.SaveChangesAsync();

            TempData["DenunciaExcluida"] = true;
            return RedirectToAction(nameof(Profile));
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string senhaAtual, string novaSenha, string confirmarSenha)
        {
            if (string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(novaSenha) || string.IsNullOrWhiteSpace(confirmarSenha))
            {
                ModelState.AddModelError("", "Preencha todos os campos.");
                return View();
            }

            if (novaSenha.Length < 6)
            {
                ModelState.AddModelError("", "A nova senha deve ter pelo menos 6 caracteres.");
                return View();
            }

            if (novaSenha != confirmarSenha)
            {
                ModelState.AddModelError("", "A confirmação da nova senha não confere.");
                return View();
            }

            var usuario = await ObterUsuarioLogadoAsync();
            if (usuario == null)
                return RedirectToAction("Login");

            if (!BCrypt.Net.BCrypt.Verify(senhaAtual, usuario.SenhaHash))
            {
                ModelState.AddModelError("", "Senha atual incorreta.");
                return View();
            }

            if (BCrypt.Net.BCrypt.Verify(novaSenha, usuario.SenhaHash))
            {
                ModelState.AddModelError("", "A nova senha deve ser diferente da senha atual.");
                return View();
            }

            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
            usuario.PasswordResetToken = null;
            usuario.PasswordResetExpires = null;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            TempData["SenhaAlterada"] = true;
            return RedirectToAction(nameof(Profile));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var usuario = await ObterUsuarioLogadoAsync();
            if (usuario == null)
                return RedirectToAction("Login");

            return View(usuario);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string nome, string cidade, string? bairro, string uf)
        {
            var usuario = await ObterUsuarioLogadoAsync();
            if (usuario == null)
                return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(uf))
            {
                ModelState.AddModelError("", "Nome, cidade e estado são obrigatórios.");
                return View(usuario);
            }

            uf = uf.Trim().ToUpperInvariant();
            if (uf.Length != 2)
            {
                ModelState.AddModelError("", "Informe a sigla do estado (ex: MG).");
                return View(usuario);
            }

            usuario.Nome = nome.Trim();
            usuario.Cidade = cidade.Trim();
            usuario.Bairro = string.IsNullOrWhiteSpace(bairro) ? null : bairro.Trim();
            usuario.Uf = uf;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            await SignInUsuario(usuario);

            TempData["PerfilAtualizado"] = true;
            return RedirectToAction(nameof(Profile));
        }

        private async Task<Usuario?> ObterUsuarioLogadoAsync()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var id))
                return null;

            return await _context.Usuarios.FindAsync(id);
        }

        private static string GeneratePasswordResetToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private async Task SignInUsuario(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome ?? usuario.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
