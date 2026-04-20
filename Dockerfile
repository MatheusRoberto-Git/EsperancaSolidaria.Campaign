FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Backend/EsperancaSolidaria.Campanha.API/EsperancaSolidaria.Campanha.API.csproj", "Backend/EsperancaSolidaria.Campanha.API/"]
COPY ["src/Backend/EsperancaSolidaria.Campanha.Application/EsperancaSolidaria.Campanha.Application.csproj", "Backend/EsperancaSolidaria.Campanha.Application/"]
COPY ["src/Backend/EsperancaSolidaria.Campanha.Domain/EsperancaSolidaria.Campanha.Domain.csproj", "Backend/EsperancaSolidaria.Campanha.Domain/"]
COPY ["src/Backend/EsperancaSolidaria.Campanha.Infrastructure/EsperancaSolidaria.Campanha.Infrastructure.csproj", "Backend/EsperancaSolidaria.Campanha.Infrastructure/"]
COPY ["src/Shared/EsperancaSolidaria.Campanha.Communication/EsperancaSolidaria.Campanha.Communication.csproj", "Shared/EsperancaSolidaria.Campanha.Communication/"]
COPY ["src/Shared/EsperancaSolidaria.Campanha.Exceptions/EsperancaSolidaria.Campanha.Exceptions.csproj", "Shared/EsperancaSolidaria.Campanha.Exceptions/"]

RUN dotnet restore "Backend/EsperancaSolidaria.Campanha.API/EsperancaSolidaria.Campanha.API.csproj"

COPY src/ .

RUN dotnet publish "Backend/EsperancaSolidaria.Campanha.API/EsperancaSolidaria.Campanha.API.csproj" \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EsperancaSolidaria.Campanha.API.dll"]