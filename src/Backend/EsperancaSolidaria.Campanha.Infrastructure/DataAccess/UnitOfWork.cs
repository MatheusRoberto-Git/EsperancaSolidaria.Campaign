using EsperancaSolidaria.Campanha.Domain.Repositories;

namespace EsperancaSolidaria.Campanha.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EsperancaSolidariaCampanhaDbContext _dbContext;

        public UnitOfWork(EsperancaSolidariaCampanhaDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}