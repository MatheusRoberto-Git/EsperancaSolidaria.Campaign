using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Entities;
using Sqids;

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

        public static IList<ResponseShortCampaignAdminJson> MapToShortCampaignAdminResponseJson(this IList<Campaign> campaigns, SqidsEncoder<long> idEncoder)
        {
            return campaigns.Select(campaign => new ResponseShortCampaignAdminJson
            {
                Id = idEncoder.Encode(campaign.Id),
                Title = campaign.Title,
                FinancialGoal = campaign.FinancialGoal,
                AmountRaised = campaign.AmountRaised,
            }).ToList();
        }
    }
}