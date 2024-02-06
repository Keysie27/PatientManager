namespace SGP.Core.Domain.Entities
{
    public class ResultadoLab
    {
        public int IdResultadoLab { get; set; }
        public string? Estado { get; set; }
        public string? Resultado { get; set; }

        //ForeignKeys:
        public int IdPaciente { get; set; }
        public int IdPruebaLab { get; set; }

        //Navigation propertys:
        public Paciente? Paciente { get; set; }
        public PruebaLab? PruebaLab { get; set; }
    }
}
