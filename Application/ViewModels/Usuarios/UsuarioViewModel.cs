using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.ViewModels.Usuarios
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string TipoUsuario { get; set; }
    }
}
