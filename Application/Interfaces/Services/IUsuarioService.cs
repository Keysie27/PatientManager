using SGP.Core.Application.ViewModels.Usuarios;

namespace SGP.Core.Application.Interfaces.Services
{
    public interface IUsuarioService : IGenericService<SaveUsuarioViewModel, UsuarioViewModel>
    {
        Task<UsuarioViewModel> Login(LoginViewModel vm);
    }
}
