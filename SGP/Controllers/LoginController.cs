using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Usuarios;
using SGP.Core.Application.Helpers;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ValidateUserSession _validateUserSession;

        public LoginController(IUsuarioService usuarioService, ValidateUserSession validateUserSession)
        {
            _usuarioService = usuarioService; 
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UsuarioViewModel usuarioVm = await _usuarioService.Login(loginVm);

            if (usuarioVm != null) 
            {
                HttpContext.Session.Set<UsuarioViewModel>("user", usuarioVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Datos de acceso incorrectos.");
            }

            return View(loginVm);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
