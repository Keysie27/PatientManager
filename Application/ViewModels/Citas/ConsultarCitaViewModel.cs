using SGP.Core.Application.ViewModels.PruebaLab;
using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.Citas
{
    public class ConsultarCitaViewModel
    {
        public int IdPaciente { get; set; }
        public int IdPruebaLab { get; set; }

        [Required(ErrorMessage = "Seleccione una Prueba de Laboratorio.")]
        public List<PruebaLabViewModel>? PruebasLab { get; set; }
    }
}
