namespace SGP.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel> 
        where SaveViewModel : class
        where ViewModel : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Update(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> GetAllViewModels();
        Task<SaveViewModel> GetViewModelById(int id);
    }
}
