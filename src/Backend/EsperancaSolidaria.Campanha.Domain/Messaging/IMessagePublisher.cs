using EsperancaSolidaria.Campanha.Domain.Events;

namespace EsperancaSolidaria.Campanha.Domain.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishDonationReceived(DonationReceivedEvent donationEvent);
    }
}