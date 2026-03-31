using FluentMigrator;

namespace EsperancaSolidaria.Campanha.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_CAMPAIGN, "Create table to save the campaign's information")]
    public class Version0000001 : VersionBase
    {        
        public override void Up()
        {
            CreateTable("Campaigns")
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("Description").AsString(2000).NotNullable()
                .WithColumn("StartDate").AsDateTime().NotNullable()
                .WithColumn("EndDate").AsDateTime().NotNullable()
                .WithColumn("FinancialGoal").AsDecimal().NotNullable()
                .WithColumn("AmountRaised").AsDecimal().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable();
        }
    }
}