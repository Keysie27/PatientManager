using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Pacientes;
using SGP.Core.Application.ViewModels.Citas;
using SGP.Core.Application.ViewModels.PruebaLab;

namespace SGP.Core.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly ICitaRepository _citaRepository;

        public CitaService(ICitaRepository repository)
        {
            _citaRepository = repository;
        }

        public async Task<SaveCitaViewModel> Add(SaveCitaViewModel vm)
        {
            Cita cita = new()
            {
                Estado = vm.Estado,
                Fecha = vm.Fecha,
                Hora = vm.Hora,
                Causa = vm.Causa,
                IdPaciente = vm.IdPaciente,
                IdMedico = vm.IdMedico,
            };

            cita = await _citaRepository.AddAsync(cita);

            SaveCitaViewModel citaVm = new()
            {
                IdCita = cita.IdCita,
                Estado = cita.Estado,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                IdPaciente = cita.IdPaciente,
                IdMedico = cita.IdMedico,
            };

            return citaVm;
        }
        
        public async Task Update(SaveCitaViewModel vm)
        {
            Cita cita = await _citaRepository.GetByIdAsync(vm.IdCita);
            cita.IdCita = vm.IdCita;
            cita.Estado = vm.Estado;
            cita.Fecha = vm.Fecha;
            cita.Hora = vm.Hora;
            cita.Causa = vm.Causa;
            cita.IdPaciente = vm.IdPaciente;
            cita.IdMedico = vm.IdMedico;

            await _citaRepository.UpdateAsync(cita);
        }
        
        public async Task Delete(int IdCita)
        {
            var cita = await _citaRepository.GetByIdAsync(IdCita);

            await _citaRepository.DeleteAsync(cita);
        }

        public async Task<List<CitaViewModel>> GetAllViewModels()
        {
            var citas = await _citaRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "Medico" });

            return citas.Select(cita => new CitaViewModel
            {
                IdCita = cita.IdCita,
                Estado = cita.Estado,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                IdPaciente = cita.IdPaciente,
                Paciente = cita.Paciente,
                IdMedico = cita.IdMedico,
                Medico = cita.Medico,

            }).ToList();
        }

        public async Task<SaveCitaViewModel> GetViewModelById(int IdCita)
        {
            var cita = await _citaRepository.GetByIdAsync(IdCita);

            SaveCitaViewModel vm = new()
            {
                IdCita = cita.IdCita,
                Estado = cita.Estado,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                Causa = cita.Causa,
                IdPaciente = cita.IdPaciente,
                IdMedico = cita.IdMedico,
            };

            return vm;
        }
    }
}
