using EsperancaSolidaria.Campanha.Domain.Enums;

namespace EsperancaSolidaria.Campanha.Domain.Entities
{
    public class Campaign : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FinancialGoal { get; set; }
        public decimal AmountRaised { get; set; }
        public CampaignStatus Status { get; set; } = CampaignStatus.Ativa;
    }
}