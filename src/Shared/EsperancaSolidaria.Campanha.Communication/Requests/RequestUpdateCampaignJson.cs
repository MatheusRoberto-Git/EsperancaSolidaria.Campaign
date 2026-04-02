using EsperancaSolidaria.Campanha.Communication.Enums;

namespace EsperancaSolidaria.Campanha.Communication.Requests
{
    public class RequestUpdateCampaignJson
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FinancialGoal { get; set; }
        public CampaignStatus Status { get; set; }
    }
}