namespace EsperancaSolidaria.Campanha.Communication.Requests
{
    public class RequestDonationJson
    {
        public string CampaignId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}