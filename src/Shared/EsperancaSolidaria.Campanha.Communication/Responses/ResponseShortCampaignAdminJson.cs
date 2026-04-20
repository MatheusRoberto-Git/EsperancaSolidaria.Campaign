namespace EsperancaSolidaria.Campanha.Communication.Responses
{
    public class ResponseShortCampaignAdminJson
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal FinancialGoal { get; set; }
        public decimal AmountRaised { get; set; }
    }
}
