using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Entities;

namespace EsperancaSolidaria.Campanha.Application.Extensions
{
    public static class CampaignListExtension
    {
        public static IList<ResponseShortCampaignJson> MapToShortCampaignResponseJson(this IList<Campaign> campaigns)
        {
            return campaigns.Select(campaign => new ResponseShortCampaignJson
            {
                Title = campaign.Title,
                FinancialGoal = campaign.FinancialGoal,
                AmountRaised = campaign.AmountRaised,
            }).ToList();
        }
    }
}