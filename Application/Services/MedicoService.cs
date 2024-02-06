using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.Medicos;

namespace SGP.Core.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository repository)
        {
            _medicoRepository = repository;
        }

        public async Task<SaveMedicoViewModel> Add(SaveMedicoViewModel vm)
        {
            Medico medico = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                Cedula = vm.Cedula,
                FotoUrl = vm.FotoUrl,
            };

            medico = await _medicoRepository.AddAsync(medico);

            SaveMedicoViewModel medicoVm = new()
            {
                IdMedico = medico.IdMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Telefono = medico.Telefono,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                FotoUrl = medico.FotoUrl,
            };

            return medicoVm;
        }
        
        public async Task Update(SaveMedicoViewModel vm)
        {
            Medico medico = await _medicoRepository.GetByIdAsync(vm.IdMedico);
            medico.IdMedico = vm.IdMedico;
            medico.Nombre = vm.Nombre;
            medico.Apellido = vm.Apellido;
            medico.Telefono = vm.Telefono;
            medico.Correo = vm.Correo;
            medico.Cedula = vm.Cedula;
            medico.FotoUrl = vm.FotoUrl;

            await _medicoRepository.UpdateAsync(medico);
        }
        
        public async Task Delete(int IdMedico)
        {
            var medico = await _medicoRepository.GetByIdAsync(IdMedico);

            await _medicoRepository.DeleteAsync(medico);
        }

        public async Task<List<MedicoViewModel>> GetAllViewModels()
        {
            var medicos = await _medicoRepository.GetAllAsync();

            return medicos.Select(medico => new MedicoViewModel
            {
                IdMedico = medico.IdMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Telefono = medico.Telefono,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                FotoUrl = medico.FotoUrl
            }).ToList();
        }     
        
        public async Task<SaveMedicoViewModel> GetViewModelById(int IdMedico)
        {
            var medico = await _medicoRepository.GetByIdAsync(IdMedico);

            SaveMedicoViewModel vm = new()
            {
                IdMedico = medico.IdMedico,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Telefono = medico.Telefono,
                Correo = medico.Correo,
                Cedula = medico.Cedula,
                FotoUrl = medico.FotoUrl
            };

            return vm;
        }
    }
}
