using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.ViewModels.Medicos
{
    public class MedicoViewModel
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cedula { get; set; }
        public string? FotoUrl { get; set; }

        //Navigation property:
        public ICollection<Cita>? Citas { get; set; }
    }
}
