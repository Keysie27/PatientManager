using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.Medicos
{
    public class SaveMedicoViewModel
    {
        public int IdMedico { get; set; }

        [Required(ErrorMessage = "Ingrese su Nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su Apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese su número de Teléfono.")]
        public string Telefono { get; set; }        
        
        [Required(ErrorMessage = "Ingrese su Correo Electrónico.")]
        public string Correo { get; set; }        
        
        [Required(ErrorMessage = "Ingrese su número de Cédula.")]
        public string Cedula { get; set; }
        
        public string? FotoUrl { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
