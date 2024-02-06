using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGP.Core.Application.Interfaces.Repositories;
using SGP.Infrastructure.Persistence.Contexts;
using SGP.Infrastructure.Persistence.Repositories;

namespace SGP.Infrastructure.Persistence
{
    // Extension methods - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

                if (useInMemoryDatabase)
                {
                    services.AddDbContext<ApplicationContext>(option =>
                                    option.UseInMemoryDatabase("ApplicationDB"));
                }   
                else
                {
                    services.AddDbContext<ApplicationContext>(option =>
                                    option.UseSqlServer(connectionString,
                                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
                }
            #endregion

            #region Repositories
            // Dependency inversion - Dependency injection
                services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                services.AddTransient<IUsuarioRepository, UsuarioRepository>(); 
                services.AddTransient<IPacienteRepository, PacienteRepository>(); 
                services.AddTransient<IMedicoRepository, MedicoRepository>(); 
                services.AddTransient<IPruebaLabRepository, PruebaLabRepository>(); 
                services.AddTransient<IResultadoLabRepository, ResultadoLabRepository>(); 
                services.AddTransient<ICitaRepository, CitaRepository>(); 
            #endregion
        }
    }
}
