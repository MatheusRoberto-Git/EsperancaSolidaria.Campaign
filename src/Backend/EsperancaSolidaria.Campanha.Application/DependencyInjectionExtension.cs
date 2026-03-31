using EsperancaSolidaria.Campanha.Application.Services.Mapster;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsperancaSolidaria.Campanha.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddMapsterConfiguration();
            AddUseCases(services);
        }

        private static void AddMapsterConfiguration()
        {
            MapConfigs.Configure();
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterCampaignUseCase, RegisterCampaignUseCase>();
        }
    }
}