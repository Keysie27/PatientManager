using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.ViewModels.ResultadoLab
{
    public class ResultadoLabViewModel
    {
        public int IdResultadoLab { get; set; }
        public string? Estado { get; set; }
        public string? Resultado { get; set; }

        //ForeignKeys:
        public int IdPaciente { get; set; }
        public int IdPruebaLab { get; set; }

        //Navigation propertys:
        public Paciente? Paciente { get; set; }
        public Core.Domain.Entities.PruebaLab? PruebaLab { get; set; }
    }
}
