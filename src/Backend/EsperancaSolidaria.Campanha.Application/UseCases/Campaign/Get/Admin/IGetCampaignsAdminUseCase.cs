using EsperancaSolidaria.Campanha.Communication.Responses;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Admin
{
    public interface IGetCampaignsAdminUseCase
    {
        Task<ResponseCampaignsAdminJson> Execute();
    }
}
