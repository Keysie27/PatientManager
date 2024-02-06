using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.Pacientes
{
    public class SavePacienteViewModel
    {
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Ingrese su Nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese su Apellido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingrese su número de Teléfono.")]
        public string Telefono { get; set; }        
        
        [Required(ErrorMessage = "Ingrese su Dirección.")]
        public string Direccion { get; set; }        
        
        [Required(ErrorMessage = "Seleccione su Fecha de Nacimiento.")]
        public DateTime FechaNacimiento { get; set; }
        
        [Required(ErrorMessage = "Ingrese su número de Cédula.")]
        public string Cedula { get; set; }
       
        public string? FotoUrl { get; set; }
        
        [Required(ErrorMessage = "Seleccione una opción.")]
        public bool Fuma { get; set; }
        
        [Required(ErrorMessage = "Seleccione una opción.")]
        public bool Alergias { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
