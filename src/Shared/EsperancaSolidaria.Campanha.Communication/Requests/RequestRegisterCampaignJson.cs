namespace EsperancaSolidaria.Campanha.Communication.Requests
{
    public class RequestRegisterCampaignJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FinancialGoal { get; set; }
    }
}