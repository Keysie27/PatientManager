using System.ComponentModel.DataAnnotations;

namespace SGP.Core.Application.ViewModels.Usuarios
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Coloque el nombre de usuario.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Coloque una contraseña.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
