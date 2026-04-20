namespace EsperancaSolidaria.Campanha.Domain.Events
{
    public class DonationReceivedEvent
    {
        public long CampaignId { get; set; }
        public decimal Amount { get; set; }
    }
}
