namespace EsperancaSolidaria.Campanha.Domain.Repositories.Campaign
{
    public interface ICampaignReadOnlyRepository
    {
        Task<IList<Entities.Campaign>> Get();
    }
}