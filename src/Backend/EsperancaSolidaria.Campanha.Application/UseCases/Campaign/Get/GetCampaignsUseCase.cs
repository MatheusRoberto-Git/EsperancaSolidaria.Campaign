using EsperancaSolidaria.Campanha.Application.Extensions;
using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get
{
    public class GetCampaignsUseCase : IGetCampaignsUseCase
    {
        private readonly ICampaignReadOnlyRepository _repository;

        public GetCampaignsUseCase(ICampaignReadOnlyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseCampaignsJson> Execute()
        {
            var campaigns = await _repository.Get();

            return new ResponseCampaignsJson
            {
                Campaigns = campaigns.MapToShortCampaignResponseJson()
            };
        }
    }
}