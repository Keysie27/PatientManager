using Microsoft.Extensions.DependencyInjection;
using SGP.Core.Application.Interfaces.Services;
using SGP.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services                
                services.AddTransient<IUsuarioService, UsuarioService>();
                services.AddTransient<IMedicoService, MedicoService>();
                services.AddTransient<IPacienteService, PacienteService>();
                services.AddTransient<IPruebaLabService, PruebaLabService>();
                services.AddTransient<IResultadoLabService, ResultadoLabService>();
                services.AddTransient<ICitaService, CitaService>();
            #endregion
        }
    }
}
