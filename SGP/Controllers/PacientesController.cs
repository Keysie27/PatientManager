using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Medicos;
using SGP.Core.Application.ViewModels.Pacientes;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly ValidateUserSession _validateUserSession;

        public PacientesController(IPacienteService pacienteService, ValidateUserSession validateUserSession)
        {
            _pacienteService = pacienteService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(await _pacienteService.GetAllViewModels());
        }

        public IActionResult CreatePaciente()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("SavePaciente", new SavePacienteViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaciente(SavePacienteViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePaciente", vm);
            }
            else
            {
                SavePacienteViewModel pacienteVm = await _pacienteService.Add(vm);

                if(pacienteVm != null && pacienteVm.IdPaciente != 0)
                {
                    pacienteVm.FotoUrl = UploadImage(vm.File, pacienteVm.IdPaciente);
                    await _pacienteService.Update(pacienteVm);
                }

                return RedirectToRoute(new { controller = "Pacientes", action = "Index" });
            }
        }

        public async Task<IActionResult> EditPaciente(int IdPaciente)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("SavePaciente", await _pacienteService.GetViewModelById(IdPaciente));
        }

        [HttpPost]
        public async Task<IActionResult> EditPaciente(SavePacienteViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePaciente", vm);
            }
            else
            {
                SavePacienteViewModel pacienteVm = await _pacienteService.GetViewModelById(vm.IdPaciente);
                vm.FotoUrl = UploadImage(vm.File, vm.IdPaciente, true, pacienteVm.FotoUrl);

                await _pacienteService.Update(vm);
                return RedirectToRoute(new { controller = "Pacientes", action = "Index" });
            }
        }

        public async Task<IActionResult> DeletePaciente(int IdPaciente)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("DeletePaciente", await _pacienteService.GetViewModelById(IdPaciente));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePacientePost(int IdPaciente)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            await _pacienteService.Delete(IdPaciente);

            // Obtener la ruta del directorio
            string basePath = $"/images/Pacientes/{IdPaciente}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Borrar todos los archivos y carpetas
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                };

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                };

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Pacientes", action = "Index" });
        }

        private string UploadImage(IFormFile file, int IdPaciente, bool isEditMode = false, string imageUrl = "")
        {
            // Si la imagen ya existe retornar su Url; 
            if (isEditMode && file == null)
            {
                return imageUrl;
            }

            // Obtener la ruta del directorio
            string basePath = $"/images/Pacientes/{IdPaciente}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Crear el diretorio de la foto si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generar el nombre que tendrá la imagen
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new (file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imageUrl.Split('/');
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if(System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}
