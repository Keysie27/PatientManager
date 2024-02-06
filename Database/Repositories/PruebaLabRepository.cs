using SGP.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;

namespace SGP.Infrastructure.Persistence.Repositories
{
    public class PruebaLabRepository : GenericRepository<PruebaLab>, IPruebaLabRepository
    {
        private readonly ApplicationContext _dbContext;

        public PruebaLabRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
