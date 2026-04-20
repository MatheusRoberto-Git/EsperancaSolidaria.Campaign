using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Domain.Events;
using EsperancaSolidaria.Campanha.Domain.Messaging;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using EsperancaSolidaria.Campanha.Exceptions;
using EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase;
using Sqids;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Donation
{
    public class DonationUseCase : IDonationUseCase
    {
        private readonly ICampaignReadOnlyRepository _repository;
        private readonly IMessagePublisher _publisher;
        private readonly SqidsEncoder<long> _idEncoder;

        public DonationUseCase(ICampaignReadOnlyRepository repository, IMessagePublisher publisher, SqidsEncoder<long> idEncoder)
        {
            _repository = repository;
            _publisher = publisher;
            _idEncoder = idEncoder;
        }

        public async Task Execute(RequestDonationJson request)
        {
            var id = _idEncoder.Decode(request.CampaignId).Single();
            var campaign = await _repository.GetById(id);

            if(campaign is null)
            {
                throw new NotFoundException(ResourceMessagesException.CAMPAIGN_NOT_FOUND);
            }                

            if(campaign.Status != Domain.Enums.CampaignStatus.Ativa)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.CAMPAIGN_ALREADY_CLOSED]);
            }                

            Validate(request);

            await _publisher.PublishDonationReceived(new DonationReceivedEvent
            {
                CampaignId = id,
                Amount = request.Amount
            });
        }

        private static void Validate(RequestDonationJson request)
        {
            var result = new DonationValidator().Validate(request);

            if(result.IsValid is false)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }                
        }
    }
}