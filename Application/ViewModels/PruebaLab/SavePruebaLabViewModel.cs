using SGP.Core.Application.ViewModels.Pacientes;
using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.PruebaLab
{
    public class SavePruebaLabViewModel
    {
        public int IdPruebaLab { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre de la prueba.")]
        public string Nombre { get; set; }

        public string? Estado { get; set; }

        public string? Resultado { get; set; }
        
        //ForeignKeys:
        [Required(ErrorMessage = "Seleccione al Paciente.")]
        public int IdPaciente { get; set; }
 

        public List<PacienteViewModel>? Pacientes { get; set; }
    }
}
