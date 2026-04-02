namespace EsperancaSolidaria.Campanha.Domain.Repositories.Campaign
{
    public interface ICampaignUpdateOnlyRepository
    {
        Task<Entities.Campaign?> GetById(long campaignId);
        void Update(Entities.Campaign campaign);
    }
}