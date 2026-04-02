using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Exceptions;
using FluentValidation;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Update
{
    public class UpdateCampaignValidator : AbstractValidator<RequestUpdateCampaignJson>
    {
        public UpdateCampaignValidator()
        {
            RuleFor(campaign => campaign.Title)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.TITLE_EMPTY);

            RuleFor(campaign => campaign.Description)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);

            RuleFor(campaign => campaign.StartDate)
                .NotEqual(default(DateTime))
                .WithMessage(ResourceMessagesException.START_DATE_EMPTY);

            RuleFor(campaign => campaign.EndDate)
                .NotEqual(default(DateTime))
                .WithMessage(ResourceMessagesException.END_DATE_EMPTY);
            When(campaign => campaign.EndDate != default(DateTime), () =>
            {
                RuleFor(campaign => campaign.EndDate)
                    .GreaterThan(DateTime.UtcNow)
                    .WithMessage(ResourceMessagesException.PASS_END_DATE);
            });

            RuleFor(campaign => campaign.FinancialGoal)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.NON_0_FINANCIAL_GOAL);

            RuleFor(campaign => campaign.Status)
                .IsInEnum()
                .WithMessage(ResourceMessagesException.INVALID_STATUS);
        }
    }
}