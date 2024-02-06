using SGP.Infrastructure.Persistence.Contexts;
using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.Helpers;
using SGP.Core.Application.ViewModels.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace SGP.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationContext _dbContext;

        public UsuarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Usuario> AddAsync(Usuario entity)
        {
            entity.Contraseña = PasswordEncryptation.ComputeSha256Hash(entity.Contraseña);
            await base.AddAsync(entity);
            return entity;
        }

        public async Task<Usuario> LoginAsync(LoginViewModel loginVm)
        {
            string passwordEncrypt = PasswordEncryptation.ComputeSha256Hash(loginVm.Contraseña);
            Usuario usuario = await _dbContext.Set<Usuario>().FirstOrDefaultAsync(usuario => usuario.NombreUsuario == loginVm.NombreUsuario && usuario.Contraseña == passwordEncrypt);
            
            return usuario;
        }
    }
}
