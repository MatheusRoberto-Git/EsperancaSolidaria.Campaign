using EsperancaSolidaria.Campanha.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsperancaSolidaria.Campanha.Infrastructure.DataAccess
{
    public class EsperancaSolidariaCampanhaDbContext : DbContext
    {
        public EsperancaSolidariaCampanhaDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EsperancaSolidariaCampanhaDbContext).Assembly);
        }
    }
}