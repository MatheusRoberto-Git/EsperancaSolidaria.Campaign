using EsperancaSolidaria.Campanha.Communication.Responses;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Public
{
    public interface IGetCampaignsUseCase
    {
        Task<ResponseCampaignsJson> Execute();
    }
}