using SGP.Core.Application.ViewModels.Pacientes;
using SGP.Core.Application.ViewModels.PruebaLab;

namespace SGP.Core.Application.ViewModels.ResultadoLab
{
    public class SaveResultadoLabViewModel
    {
        public int IdResultadoLab { get; set; }

        public string? Estado { get; set; }

        public string? Resultado { get; set; }
     
        public int IdPaciente { get; set; }
        public int IdPruebaLab { get; set; }

        public List<PacienteViewModel>? Pacientes { get; set; }
        public List<PruebaLabViewModel>? PruebasLab { get; set; }
    }
}
