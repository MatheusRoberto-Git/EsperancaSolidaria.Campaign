namespace EsperancaSolidaria.Campanha.Communication.Responses
{
    public class ResponseShortCampaignJson
    {
        public string Title { get; set; } = string.Empty;
        public decimal FinancialGoal { get; set; }
        public decimal AmountRaised { get; set; }
    }
}