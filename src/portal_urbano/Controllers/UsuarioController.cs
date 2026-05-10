using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoUrbano.Data;
using ProjetoUrbano.Models;
using ProjetoUrbano.Services.Email;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoUrbano.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public UsuarioController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            usuario.SenhaHash = HashSenha(usuario.Senha);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return RedirectToAction("Cadastro");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var senhaHash = HashSenha(senha);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.SenhaHash == senhaHash);

            if (usuario == null)
            {
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetString("UsuarioId", usuario.IdUsuario.ToString());
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            return RedirectToAction("Perfil", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciSenha(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Erro = "Informe um email válido.";
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                ViewBag.Sucesso = "Se o email existir, enviaremos as instruções de recuperação.";
                return View();
            }

            usuario.ResetToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            usuario.ResetTokenExpiraEm = DateTime.UtcNow.AddHours(2);
            await _context.SaveChangesAsync();

            var link = Url.Action(
                "RedefinirSenha",
                "Usuario",
                new { token = usuario.ResetToken, email = usuario.Email },
                Request.Scheme);

            var corpo = $"<p>Você solicitou a recuperação de senha.</p><p><a href=\"{link}\">Clique aqui para redefinir sua senha</a></p>";
            await _emailService.EnviarAsync(usuario.Email, "Recuperação de senha", corpo);

            ViewBag.Sucesso = "Se o email existir, enviaremos as instruções de recuperação.";
            return View();
        }

        public IActionResult RedefinirSenha(string token, string email)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(string token, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Erro = "Preencha todos os campos.";
                ViewBag.Token = token;
                ViewBag.Email = email;
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.ResetToken == token);
            if (usuario == null || usuario.ResetTokenExpiraEm == null || usuario.ResetTokenExpiraEm < DateTime.UtcNow)
            {
                ViewBag.Erro = "Link inválido ou expirado.";
                return View();
            }

            usuario.SenhaHash = HashSenha(senha);
            usuario.ResetToken = null;
            usuario.ResetTokenExpiraEm = null;
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Senha redefinida com sucesso. Faça login.";
            return RedirectToAction("Login");
        }

        private static string HashSenha(string senha)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha ?? string.Empty));
            return Convert.ToHexString(bytes);
        }
    }
}