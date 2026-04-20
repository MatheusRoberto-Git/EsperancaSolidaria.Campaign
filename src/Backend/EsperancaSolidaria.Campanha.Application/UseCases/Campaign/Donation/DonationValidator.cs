using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Exceptions;
using FluentValidation;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Donation
{
    public class DonationValidator: AbstractValidator<RequestDonationJson>
    {
        public DonationValidator()
        {
            RuleFor(d => d.CampaignId)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.CAMPAIGN_ID_EMPTY);

            RuleFor(d => d.Amount)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.DONATION_AMOUNT_INVALID);
        }
    }
}