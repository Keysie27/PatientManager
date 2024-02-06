using SGP.Core.Application.ViewModels.Usuarios;
using SGP.Core.Application.Helpers;

namespace SGP.WebApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UsuarioViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsuarioViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }

    }
}
