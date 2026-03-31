using Microsoft.Extensions.Configuration;

namespace EsperancaSolidaria.Campanha.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("Connection")!;
        }
    }
}