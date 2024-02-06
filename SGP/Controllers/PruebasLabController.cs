using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.PruebaLab;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class PruebasLabController : Controller
    {
        private readonly IPruebaLabService _pruebaLabService;
        private readonly ValidateUserSession _validateUserSession;

        public PruebasLabController(IPruebaLabService pruebaLabService, IPacienteService pacienteService, ValidateUserSession validateUserSession)
        {
            _pruebaLabService = pruebaLabService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(await _pruebaLabService.GetAllViewModels());
        }

        public IActionResult CreatePruebaLab()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("SavePruebaLab", new SavePruebaLabViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePruebaLab(SavePruebaLabViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePruebaLab", vm);
            }
            else
            {
                await _pruebaLabService.Add(vm);
                return RedirectToRoute(new { controller = "PruebasLab", action = "Index" });
            }
        }

        public async Task<IActionResult> EditPruebaLab(int IdPruebaLab)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            SavePruebaLabViewModel vm = await _pruebaLabService.GetViewModelById(IdPruebaLab);
            return View("SavePruebaLab", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPruebaLab(SavePruebaLabViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePruebaLab", vm);
            }
            else
            {
                await _pruebaLabService.Update(vm);
                return RedirectToRoute(new { controller = "PruebasLab", action = "Index" });
            }
        }

        public async Task<IActionResult> DeletePruebaLab(int IdPruebaLab)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("DeletePruebaLab", await _pruebaLabService.GetViewModelById(IdPruebaLab));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePruebaLabPost(int IdPruebaLab)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            await _pruebaLabService.Delete(IdPruebaLab);

            return RedirectToRoute(new { controller = "PruebasLab", action = "Index" });
        }
    }
}
