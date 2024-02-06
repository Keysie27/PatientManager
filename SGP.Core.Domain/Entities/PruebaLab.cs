namespace SGP.Core.Domain.Entities
{
    public class PruebaLab
    {
        public int IdPruebaLab { get; set; }
        public string Nombre { get; set; }

        // Navigation propertys
        public ICollection<ResultadoLab>? ResultadosLab { get; set; }

    }
}
