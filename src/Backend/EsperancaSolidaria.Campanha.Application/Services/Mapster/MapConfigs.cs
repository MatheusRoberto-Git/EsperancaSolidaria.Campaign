using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Domain.Entities;
using Mapster;

namespace EsperancaSolidaria.Campanha.Application.Services.Mapster
{
    public static class MapConfigs
    {
        public static void Configure()
        {
            TypeAdapterConfig<RequestRegisterCampaignJson, Campaign>
                .NewConfig()
                .Ignore(dest => dest.AmountRaised);
        }
    }
}