using EsperancaSolidaria.Campanha.Communication.Requests;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Update
{
    public interface IUpdateCampaignUseCase
    {
        Task Execute(long campaignId, RequestUpdateCampaignJson request);
    }
}