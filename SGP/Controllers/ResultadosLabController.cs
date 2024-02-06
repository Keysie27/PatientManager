using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.PruebaLab;
using SGP.Core.Application.ViewModels.ResultadoLab;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class ResultadosLabController : Controller
    {
        private readonly IResultadoLabService _resultadoLabService;
        private readonly ValidateUserSession _validateUserSession;

        public ResultadosLabController(IResultadoLabService resultadoLabService, IPruebaLabService pruebaLabService, IPacienteService pacienteService, ValidateUserSession validateUserSession)
        {
            _resultadoLabService = resultadoLabService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(await _resultadoLabService.GetAllViewModels());
        }

        public async Task<IActionResult> ReportarResultados(int IdResultadoLab)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("ReportarResultados", await _resultadoLabService.GetViewModelById(IdResultadoLab));
        }

        [HttpPost]
        public async Task<IActionResult> ReportarResultados(SaveResultadoLabViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("ReportarResultados", vm);
            }
            else
            {
                vm.Estado = "Completado";
                await _resultadoLabService.Update(vm);
                return RedirectToRoute(new { controller = "ResultadosLab", action = "Index" });
            }
        }
    }
}
