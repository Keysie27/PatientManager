using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Pacientes;

namespace SGP.Core.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository repository)
        {
            _pacienteRepository = repository;
        }

        public async Task<SavePacienteViewModel> Add(SavePacienteViewModel vm)
        {
            Paciente paciente = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion,
                FechaNacimiento = vm.FechaNacimiento,
                Cedula = vm.Cedula,
                FotoUrl = vm.FotoUrl,
                Fuma = vm.Fuma,
                Alergias = vm.Alergias,
            };

            paciente = await _pacienteRepository.AddAsync(paciente);

            SavePacienteViewModel pacienteVm = new()
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                FechaNacimiento = paciente.FechaNacimiento,
                Cedula = paciente.Cedula,
                FotoUrl = paciente.FotoUrl,
                Fuma = paciente.Fuma,
                Alergias = paciente.Alergias,
            };

            return pacienteVm;
        }
        
        public async Task Update(SavePacienteViewModel vm)
        {
            Paciente paciente = await _pacienteRepository.GetByIdAsync(vm.IdPaciente);
            paciente.IdPaciente = vm.IdPaciente;
            paciente.Nombre = vm.Nombre;
            paciente.Apellido = vm.Apellido;
            paciente.Telefono = vm.Telefono;
            paciente.Direccion = vm.Direccion;
            paciente.FechaNacimiento = vm.FechaNacimiento;
            paciente.Cedula = vm.Cedula;
            paciente.FotoUrl = vm.FotoUrl;
            paciente.Fuma = vm.Fuma;
            paciente.Alergias = vm.Alergias;

            await _pacienteRepository.UpdateAsync(paciente);
        }
        
        public async Task Delete(int IdPaciente)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(IdPaciente);

            await _pacienteRepository.DeleteAsync(paciente);
        }

        public async Task<List<PacienteViewModel>> GetAllViewModels()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();

            return pacientes.Select(paciente => new PacienteViewModel
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                FechaNacimiento = paciente.FechaNacimiento,
                Cedula = paciente.Cedula,
                FotoUrl = paciente.FotoUrl,
                Fuma = paciente.Fuma,
                Alergias = paciente.Alergias,
            }).ToList();
        }     
        
        public async Task<SavePacienteViewModel> GetViewModelById(int IdPaciente)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(IdPaciente);

            SavePacienteViewModel vm = new()
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
                FechaNacimiento = paciente.FechaNacimiento,
                Cedula = paciente.Cedula,
                FotoUrl = paciente.FotoUrl,
                Fuma = paciente.Fuma,
                Alergias = paciente.Alergias,
            };

            return vm;
        }
    }
}
