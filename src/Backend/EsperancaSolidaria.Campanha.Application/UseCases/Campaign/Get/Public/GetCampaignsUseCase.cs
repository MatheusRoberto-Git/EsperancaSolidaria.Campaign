using EsperancaSolidaria.Campanha.Application.Extensions;
using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using Sqids;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Public
{
    public class GetCampaignsUseCase : IGetCampaignsUseCase
    {
        private readonly ICampaignReadOnlyRepository _repository;
        private readonly SqidsEncoder<long> _idEncoder;

        public GetCampaignsUseCase(ICampaignReadOnlyRepository repository, SqidsEncoder<long> idEncoder)
        {
            _repository = repository;
            _idEncoder = idEncoder;
        }

        public async Task<ResponseCampaignsJson> Execute()
        {
            var campaigns = await _repository.Get();

            return new ResponseCampaignsJson
            {
                Campaigns = campaigns.MapToShortCampaignResponseJson(_idEncoder)
            };
        }
    }
}