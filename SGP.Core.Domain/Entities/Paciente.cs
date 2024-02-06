namespace SGP.Core.Domain.Entities
{
    public class Paciente
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
        public ICollection<ResultadoLab>? ResultadosLab { get; set; }
        public ICollection<Cita>? Citas { get; set; }
    }
}
