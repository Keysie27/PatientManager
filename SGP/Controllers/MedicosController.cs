using Microsoft.AspNetCore.Mvc;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Medicos;
using SGP.WebApp.Middlewares;

namespace SGP.Core.Application.Controllers
{
    public class MedicosController : Controller
    {
        private readonly IMedicoService _medicoService;
        private readonly ValidateUserSession _validateUserSession;

        public MedicosController(IMedicoService medicoService, ValidateUserSession validateUserSession)
        {
            _medicoService = medicoService; 
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View(await _medicoService.GetAllViewModels());
        }

        public IActionResult CreateMedico()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View("SaveMedico", new SaveMedicoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedico(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveMedico", vm);
            }
            else
            {
                SaveMedicoViewModel medicoVm = await _medicoService.Add(vm);

                if(medicoVm != null && medicoVm.IdMedico != 0)
                {
                    medicoVm.FotoUrl = UploadImage(vm.File, medicoVm.IdMedico);
                    await _medicoService.Update(medicoVm);
                }

                return RedirectToRoute(new { controller = "Medicos", action = "Index" });
            }
        }

        public async Task<IActionResult> EditMedico(int IdMedico)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("SaveMedico", await _medicoService.GetViewModelById(IdMedico));
        }

        [HttpPost]
        public async Task<IActionResult> EditMedico(SaveMedicoViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveMedico", vm);
            }
            else
            {
                SaveMedicoViewModel medicoVm = await _medicoService.GetViewModelById(vm.IdMedico);
                vm.FotoUrl = UploadImage(vm.File, vm.IdMedico, true, medicoVm.FotoUrl);

                await _medicoService.Update(vm);
                return RedirectToRoute(new { controller = "Medicos", action = "Index" });
            }
        }

        public async Task<IActionResult> DeleteMedico(int IdMedico)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            return View("DeleteMedico", await _medicoService.GetViewModelById(IdMedico));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicoPost(int IdMedico)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            await _medicoService.Delete(IdMedico);

            // Obtener la ruta del directorio
            string basePath = $"/images/Medicos/{IdMedico}";
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

            return RedirectToRoute(new { controller = "Medicos", action = "Index" });
        }

        private string UploadImage(IFormFile file, int IdMedico, bool isEditMode = false, string imageUrl = "")
        {
            // Si la imagen ya existe retornar su Url; 
            if (isEditMode && file == null)
            {
                return imageUrl;
            }

            // Obtener la ruta del directorio
            string basePath = $"/images/Medicos/{IdMedico}";
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
