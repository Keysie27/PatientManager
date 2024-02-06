using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.PruebaLab;

namespace SGP.Core.Application.Services
{
    public class PruebaLabService : IPruebaLabService
    {
        private readonly IPruebaLabRepository _pruebaLabRepository;

        public PruebaLabService(IPruebaLabRepository repository)
        {
            _pruebaLabRepository = repository;
        }

        public async Task<SavePruebaLabViewModel> Add(SavePruebaLabViewModel vm)
        {
            PruebaLab pruebaLab = new()
            {
                Nombre = vm.Nombre,
            };

            pruebaLab = await _pruebaLabRepository.AddAsync(pruebaLab);

            SavePruebaLabViewModel pruebaLabVm = new()
            {
                IdPruebaLab = pruebaLab.IdPruebaLab,
                Nombre = pruebaLab.Nombre,
            };

            return pruebaLabVm;
        }
        
        public async Task Update(SavePruebaLabViewModel vm)
        {
            PruebaLab pruebaLab = await _pruebaLabRepository.GetByIdAsync(vm.IdPruebaLab);
            pruebaLab.IdPruebaLab = vm.IdPruebaLab;
            pruebaLab.Nombre = vm.Nombre;

            await _pruebaLabRepository.UpdateAsync(pruebaLab);
        }
        
        public async Task Delete(int IdPruebaLab)
        {
            var pruebaLab = await _pruebaLabRepository.GetByIdAsync(IdPruebaLab);

            await _pruebaLabRepository.DeleteAsync(pruebaLab);
        }

        public async Task<List<PruebaLabViewModel>> GetAllViewModels()
        {
            var pruebasLab = await _pruebaLabRepository.GetAllAsync();

            return pruebasLab.Select(pruebaLab => new PruebaLabViewModel
            {
                IdPruebaLab = pruebaLab.IdPruebaLab,
                Nombre = pruebaLab.Nombre,
            }).ToList();
        } 
        
        public async Task<List<PruebaLabViewModel>> GetAllViewModelsWithFilters(FilterPruebaLabViewModel filters)
        {
            var pruebasLab = await _pruebaLabRepository.GetAllWithIncludeAsync(new List<string> { "Paciente" });

            var listViewModels = pruebasLab.Select(pruebaLab => new PruebaLabViewModel
            {
                IdPruebaLab = pruebaLab.IdPruebaLab,
                Nombre = pruebaLab.Nombre,
            }).ToList();

            if (filters.IdPaciente != null)
            {
                listViewModels = listViewModels.Where(pruebaLab => pruebaLab.IdPaciente == filters.IdPaciente.Value).ToList();
            }

            return listViewModels;
        }     
        
        public async Task<SavePruebaLabViewModel> GetViewModelById(int IdPruebaLab)
        {
            var pruebaLab = await _pruebaLabRepository.GetByIdAsync(IdPruebaLab);

            SavePruebaLabViewModel vm = new()
            {
                IdPruebaLab = pruebaLab.IdPruebaLab,
                Nombre = pruebaLab.Nombre,
            };

            return vm;
        }
    }
}
