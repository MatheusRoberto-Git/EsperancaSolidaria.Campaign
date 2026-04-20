using EsperancaSolidaria.Campanha.Application.Extensions;
using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using Sqids;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Admin
{
    public class GetCampaignsAdminUseCase : IGetCampaignsAdminUseCase
    {
        private readonly ICampaignReadOnlyRepository _repository;
        private readonly SqidsEncoder<long> _idEncoder;

        public GetCampaignsAdminUseCase(ICampaignReadOnlyRepository repository, SqidsEncoder<long> idEncoder)
        {
            _repository = repository;
            _idEncoder = idEncoder;
        }

        public async Task<ResponseCampaignsAdminJson> Execute()
        {
            var campaigns = await _repository.Get();

            return new ResponseCampaignsAdminJson
            {
                Campaigns = campaigns.MapToShortCampaignAdminResponseJson(_idEncoder)
            };
        }
    }
}
