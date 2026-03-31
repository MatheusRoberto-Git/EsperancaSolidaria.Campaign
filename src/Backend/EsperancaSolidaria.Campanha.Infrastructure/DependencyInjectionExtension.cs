using EsperancaSolidaria.Campanha.Domain.Repositories;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using EsperancaSolidaria.Campanha.Infrastructure.DataAccess;
using EsperancaSolidaria.Campanha.Infrastructure.DataAccess.Repositories;
using EsperancaSolidaria.Campanha.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EsperancaSolidaria.Campanha.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);

            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<EsperancaSolidariaCampanhaDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("EsperancaSolidaria.Campanha.Infrastructure")).For.All();
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICampaignWriteOnlyRepository, CampaignRepositories>();
        }        
    }
}