using SGP.Core.Domain.Entities;

namespace SGP.Core.Application.Interfaces.Repositories
{
    public interface IPacienteRepository : IGenericRepository<Paciente>
    {
        // Aquí puedo agregar nuevos métodos aparte de los del Generic si los necesito. 
    }
}
