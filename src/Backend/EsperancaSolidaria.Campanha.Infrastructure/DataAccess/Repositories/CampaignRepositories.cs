using EsperancaSolidaria.Campanha.Domain.Entities;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;

namespace EsperancaSolidaria.Campanha.Infrastructure.DataAccess.Repositories
{
    public class CampaignRepositories : ICampaignWriteOnlyRepository
    {
        private readonly EsperancaSolidariaCampanhaDbContext _dbContext;

        public CampaignRepositories(EsperancaSolidariaCampanhaDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Campaign campaign) => await _dbContext.Campaigns.AddAsync(campaign);
    }
}