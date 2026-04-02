using EsperancaSolidaria.Campanha.Domain.Entities;
using EsperancaSolidaria.Campanha.Domain.Enums;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using Microsoft.EntityFrameworkCore;

namespace EsperancaSolidaria.Campanha.Infrastructure.DataAccess.Repositories
{
    public class CampaignRepositories : ICampaignWriteOnlyRepository, ICampaignReadOnlyRepository
    {
        private readonly EsperancaSolidariaCampanhaDbContext _dbContext;

        public CampaignRepositories(EsperancaSolidariaCampanhaDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Campaign campaign) => await _dbContext.Campaigns.AddAsync(campaign);

        public async Task<IList<Campaign>> Get()
        {
            return await _dbContext
                .Campaigns
                .AsNoTracking()
                .Where(c => c.Status == CampaignStatus.Ativa && c.Active)
                .OrderBy(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}