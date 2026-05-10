using Microsoft.AspNetCore.Mvc;
using ProjetoUrbano.Models;
using System.Diagnostics;

namespace ProjetoUrbano.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Usuario");
        }

        public IActionResult Perfil()
        {
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome") ?? "Usuário";
            return View();
        }

        public IActionResult Feed()
        {
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome") ?? "Usuário";
            return View();
        }

        public IActionResult CriarDenuncia()
        {
            ViewBag.UsuarioNome = HttpContext.Session.GetString("UsuarioNome") ?? "Usuário";
            return View();
        }

        public IActionResult Mapa()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}