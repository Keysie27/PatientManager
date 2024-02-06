using SGP.Core.Application.ViewModels.Medicos;
using SGP.Core.Application.ViewModels.Pacientes;
using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.Citas
{
    public class SaveCitaViewModel
    {
        public int IdCita { get; set; }

        public string? Estado { get; set; }

        [Required(ErrorMessage = "Ingrese la Fecha de la cita.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Ingrese la Hora de la cita.")]
        public TimeSpan Hora { get; set; }        
        
        [Required(ErrorMessage = "Ingrese la Causa de la cita.")]
        public string Causa { get; set; }

        //ForeignKeys:
        [Required(ErrorMessage = "Seleccione al Paciente.")]
        public int IdPaciente { get; set; }
        
        [Required(ErrorMessage = "Seleccione al Médico.")]
        public int IdMedico { get; set; }

        public List<PacienteViewModel>? Pacientes { get; set; }
        public List<MedicoViewModel>? Medicos { get; set; }
    }
}
