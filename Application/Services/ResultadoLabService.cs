using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.ViewModels.ResultadoLab;

namespace SGP.Core.Application.Services
{
    public class ResultadoLabService : IResultadoLabService
    {
        private readonly IResultadoLabRepository _resultadoLabRepository;

        public ResultadoLabService(IResultadoLabRepository repository)
        {
            _resultadoLabRepository = repository;
        }

        public async Task<SaveResultadoLabViewModel> Add(SaveResultadoLabViewModel vm)
        {
            ResultadoLab resultadoLab = new()
            {
                Estado = vm.Estado,
                Resultado = vm.Resultado,
                IdPaciente = vm.IdPaciente,
                IdPruebaLab = vm.IdPruebaLab,
            };

            resultadoLab = await _resultadoLabRepository.AddAsync(resultadoLab);

            SaveResultadoLabViewModel resultadoLabVm = new()
            {
                IdResultadoLab = resultadoLab.IdResultadoLab,
                Estado = resultadoLab.Estado,
                Resultado = resultadoLab.Resultado,
                IdPaciente = resultadoLab.IdPaciente,
                IdPruebaLab = resultadoLab.IdPruebaLab,
            };

            return resultadoLabVm;
        }
        
        public async Task Update(SaveResultadoLabViewModel vm)
        {
            ResultadoLab resultadoLab = await _resultadoLabRepository.GetByIdAsync(vm.IdResultadoLab);
            resultadoLab.IdResultadoLab = vm.IdResultadoLab;
            resultadoLab.Estado = vm.Estado;
            resultadoLab.Resultado = vm.Resultado;
            resultadoLab.IdPaciente = vm.IdPaciente;
            resultadoLab.IdPruebaLab = vm.IdPruebaLab;

            await _resultadoLabRepository.UpdateAsync(resultadoLab);
        }
        
        public async Task Delete(int IdResultadoLab)
        {
            var resultadoLab = await _resultadoLabRepository.GetByIdAsync(IdResultadoLab);

            await _resultadoLabRepository.DeleteAsync(resultadoLab);
        }

        public async Task<List<ResultadoLabViewModel>> GetAllViewModels()
        {
            var resultadosLab = await _resultadoLabRepository.GetAllWithIncludeAsync(new List<string> { "Paciente", "PruebaLab" });

            return resultadosLab.Select(resultadoLab => new ResultadoLabViewModel
            {
                IdResultadoLab = resultadoLab.IdResultadoLab,
                Estado = resultadoLab.Estado,
                Resultado = resultadoLab.Resultado,
                IdPaciente = resultadoLab.IdPaciente,
                Paciente = resultadoLab.Paciente,
                IdPruebaLab = resultadoLab.IdPruebaLab,
                PruebaLab = resultadoLab.PruebaLab,
            }).ToList();
        } 
        
        public async Task<SaveResultadoLabViewModel> GetViewModelById(int IdResultadoLab)
        {
            var resultadoLab = await _resultadoLabRepository.GetByIdAsync(IdResultadoLab);

            SaveResultadoLabViewModel vm = new()
            {
                IdResultadoLab = resultadoLab.IdResultadoLab,
                Estado = resultadoLab.Estado,
                Resultado = resultadoLab.Resultado,
                IdPaciente = resultadoLab.IdPaciente,
                IdPruebaLab = resultadoLab.IdPruebaLab,
            };

            return vm;
        }
    }
}
