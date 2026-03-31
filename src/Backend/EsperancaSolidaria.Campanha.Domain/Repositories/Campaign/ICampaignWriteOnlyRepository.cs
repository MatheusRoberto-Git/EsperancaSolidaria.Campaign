namespace EsperancaSolidaria.Campanha.Domain.Repositories.Campaign
{
    public interface ICampaignWriteOnlyRepository
    {
        public Task Add(Domain.Entities.Campaign campaign);
    }
}