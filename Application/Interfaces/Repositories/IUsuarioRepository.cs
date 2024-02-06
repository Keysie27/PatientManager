using SGP.Core.Application.ViewModels.Usuarios;
using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario> LoginAsync(LoginViewModel loginVm);
    }
}
