using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Core.Application.ViewModels.Usuarios;
using SGP.Core.Application.Interfaces.Services;
using System.Numerics;
using System.Xml.Linq;

namespace SGP.Core.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public async Task<UsuarioViewModel> Login(LoginViewModel vm)
        {
            Usuario usuario = await _usuarioRepository.LoginAsync(vm);

            if (usuario == null)
            {
                return null;
            }

            UsuarioViewModel userVm = new()
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                Contraseña = usuario.Contraseña,
                TipoUsuario = usuario.TipoUsuario,
            };

            return userVm;
        }

        public async Task<SaveUsuarioViewModel> Add(SaveUsuarioViewModel vm)
        {
            Usuario usuario = new()
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                NombreUsuario = vm.NombreUsuario,
                Correo = vm.Correo,
                Contraseña = vm.Contraseña,
                TipoUsuario = vm.TipoUsuario,
            };
            
            usuario = await _usuarioRepository.AddAsync(usuario);

            SaveUsuarioViewModel usuarioVm = new()
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                Contraseña = usuario.Contraseña,
            };

            return usuarioVm;
        }
        
        public async Task Update(SaveUsuarioViewModel vm)
        {
            Usuario usuario = await _usuarioRepository.GetByIdAsync(vm.IdUsuario);
            usuario.IdUsuario = vm.IdUsuario;
            usuario.Nombre = vm.Nombre;
            usuario.Apellido = vm.Apellido;
            usuario.NombreUsuario = vm.NombreUsuario;
            usuario.Correo = vm.Correo;
            usuario.Contraseña = vm.Contraseña;
            
            await _usuarioRepository.UpdateAsync(usuario);
        }
        
        public async Task Delete(int IdUsuario)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(IdUsuario);

            await _usuarioRepository.DeleteAsync(usuario);
        }

        public async Task<List<UsuarioViewModel>> GetAllViewModels()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(usuario => new UsuarioViewModel
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                Contraseña = usuario.Contraseña,
                TipoUsuario = usuario.TipoUsuario
            }).ToList();
        }     
        
        public async Task<SaveUsuarioViewModel> GetViewModelById(int IdUsuario)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(IdUsuario);

            SaveUsuarioViewModel vm = new()
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                Contraseña = usuario.Contraseña,
                TipoUsuario = usuario.TipoUsuario
            };

            return vm;
        }
    }
}
