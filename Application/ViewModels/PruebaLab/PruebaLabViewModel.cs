using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.ViewModels.PruebaLab
{
    public class PruebaLabViewModel
    {
        public int IdPruebaLab { get; set; }
        public string Nombre { get; set; }
        public string? Estado { get; set; }
        public string? Resultado { get; set; }

        //ForeignKeys:
        public int IdPaciente { get; set; }

        //Navigation propertys:
        public Paciente? Paciente { get; set; }
    }
}
