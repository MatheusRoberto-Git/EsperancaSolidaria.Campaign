using EsperancaSolidaria.Campanha.Communication.Responses;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get
{
    public interface IGetCampaignsUseCase
    {
        Task<ResponseCampaignsJson> Execute();
    }
}