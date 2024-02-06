using Microsoft.AspNetCore.Mvc;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
        } 
        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View();
        }
    }
}