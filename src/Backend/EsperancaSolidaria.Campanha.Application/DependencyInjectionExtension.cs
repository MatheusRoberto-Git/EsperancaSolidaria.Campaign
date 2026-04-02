using EsperancaSolidaria.Campanha.Application.Services.Mapster;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;

namespace EsperancaSolidaria.Campanha.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddMapsterConfiguration();
            AddIdEncoder(services, configuration);
            AddUseCases(services);
        }

        private static void AddMapsterConfiguration()
        {
            MapConfigs.Configure();
        }

        private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
        {
            var sqids = new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = configuration.GetValue<string>("Settings:IdCryptographyAlphabet")!
            });

            services.AddSingleton(sqids);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterCampaignUseCase, RegisterCampaignUseCase>();
            services.AddScoped<IGetCampaignsUseCase, GetCampaignsUseCase>();
            services.AddScoped<IUpdateCampaignUseCase, UpdateCampaignUseCase>();
        }
    }
}