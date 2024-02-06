using SGP.Infrastructure.Persistence.Contexts;
using SGP.Core.Domain.Entities;
using SGP.Core.Application.Interfaces.Repositories;

namespace SGP.Infrastructure.Persistence.Repositories
{
    public class ResultadoLabRepository : GenericRepository<ResultadoLab>, IResultadoLabRepository
    {
        private readonly ApplicationContext _dbContext;

        public ResultadoLabRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
