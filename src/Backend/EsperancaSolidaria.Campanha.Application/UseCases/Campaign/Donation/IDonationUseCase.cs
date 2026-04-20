using EsperancaSolidaria.Campanha.Communication.Requests;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Donation
{
    public interface IDonationUseCase
    {
        Task Execute(RequestDonationJson request);
    }
}