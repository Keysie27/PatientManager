using SGP.Core.Application.ViewModels.Citas;
using SGP.Core.Application.ViewModels.PruebaLab;
using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.ViewModels.Pacientes
{
    public class PacienteViewModel
    {
        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Cedula { get; set; }
        public string? FotoUrl { get; set; }
        public bool Fuma { get; set; }
        public bool Alergias { get; set; }

        //Navigation property:
        public List<PruebaLabViewModel>? PruebasLab { get; set; }
        public List<CitaViewModel>? Citas { get; set; }
    }
}
