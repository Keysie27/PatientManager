using SGP.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;

namespace SGP.Infrastructure.Persistence.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>, IMedicoRepository
    {
        private readonly ApplicationContext _dbContext;

        public MedicoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
