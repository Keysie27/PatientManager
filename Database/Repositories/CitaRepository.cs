using SGP.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;

namespace SGP.Infrastructure.Persistence.Repositories
{
    public class CitaRepository : GenericRepository<Cita>, ICitaRepository
    {
        private readonly ApplicationContext _dbContext;

        public CitaRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
