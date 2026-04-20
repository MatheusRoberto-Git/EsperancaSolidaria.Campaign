using EsperancaSolidaria.Campanha.API.Converters;
using EsperancaSolidaria.Campanha.API.Filters;
using EsperancaSolidaria.Campanha.API.Middleware;
using EsperancaSolidaria.Campanha.Application;
using EsperancaSolidaria.Campanha.Infrastructure;
using EsperancaSolidaria.Campanha.Infrastructure.DataAccess;
using EsperancaSolidaria.Campanha.Infrastructure.Extensions;
using EsperancaSolidaria.Campanha.Infrastructure.Migrations;
using Microsoft.OpenApi;

const string AUTHENTICATION_SCHEME = "Bearer";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<IdsFilter>();

    options.AddSecurityDefinition(AUTHENTICATION_SCHEME, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = AUTHENTICATION_SCHEME,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference(AUTHENTICATION_SCHEME, document)] = []
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHealthChecks();
builder.Services.AddHealthChecks().AddDbContextCheck<EsperancaSolidariaCampanhaDbContext>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health");

MigrateDatabase();

await app.RunAsync();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigrations.Migrate(connectionString, serviceScope.ServiceProvider);
}