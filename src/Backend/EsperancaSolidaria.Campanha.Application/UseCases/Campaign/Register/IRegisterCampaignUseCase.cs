using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Communication.Responses;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register
{
    public interface IRegisterCampaignUseCase
    {
        public Task<ResponseRegisteredCampaignJson> Execute(RequestRegisterCampaignJson request);
    }
}
