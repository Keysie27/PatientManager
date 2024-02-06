using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Citas;
using SGP.Core.Application.ViewModels.ResultadoLab;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;
        private readonly IPruebaLabService _pruebaLabService;
        private readonly IResultadoLabService _resultadoLabService;
        private readonly ValidateUserSession _validateUserSession;

        public CitasController(IResultadoLabService resultadoLabService, ICitaService citaService, IPacienteService pacienteService, IMedicoService medicoService, IPruebaLabService pruebaLabService, ValidateUserSession validateUserSession)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _pruebaLabService = pruebaLabService;
            _resultadoLabService = resultadoLabService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(await _citaService.GetAllViewModels());
        }

        public async Task<IActionResult> CreateCita()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            SaveCitaViewModel vm = new()
            {
                Pacientes = await _pacienteService.GetAllViewModels(),
                Medicos = await _medicoService.GetAllViewModels()
            };
            return View("SaveCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCita(SaveCitaViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveCita", vm);
            }
            else
            {
                vm.Estado = "Pend. de consulta";
                await _citaService.Add(vm);

                return RedirectToRoute(new { controller = "Citas", action = "Index" });
            }
        }

        public async Task<IActionResult> EditCita(int IdCita)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            SaveCitaViewModel vm = await _citaService.GetViewModelById(IdCita);
            vm.Pacientes = await _pacienteService.GetAllViewModels();
            vm.Medicos = await _medicoService.GetAllViewModels();
            return View("SaveCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditCita(SaveCitaViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveCita", vm);
            }
            else
            {
                await _citaService.Update(vm);
                return RedirectToRoute(new { controller = "Citas", action = "Index" });
            }
        }

        public async Task<IActionResult> DeleteCita(int IdCita)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("DeleteCita", await _citaService.GetViewModelById(IdCita));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCitaPost(int IdCita)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            await _citaService.Delete(IdCita);

            return RedirectToRoute(new { controller = "Citas", action = "Index" });
        }
        public async Task<IActionResult> ConsultarCita(int IdPaciente)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            ConsultarCitaViewModel vm = new()
            {
                IdPaciente = IdPaciente,
                PruebasLab = await _pruebaLabService.GetAllViewModels()
            };

            return View("ConsultarCita", vm);
        }

        [HttpPost]
        public async Task<IActionResult> ConsultarCita(int IdPaciente, int IdPruebaLab)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("ConsultarCita");
            }
            else
            {
                //SaveCitaViewModel cita = await _citaService.GetViewModelById(IdCita);
                //cita.Estado = "Pend. de resultados";
                //await _citaService.Update(cita);

                SaveResultadoLabViewModel vm = new()
                {
                    Estado = "Pendiente",
                    IdPaciente = IdPaciente,
                    IdPruebaLab = IdPruebaLab,
                };
                await _resultadoLabService.Add(vm);

                return RedirectToRoute(new { controller = "Citas", action = "Index" });
            }
        }
    }
}
