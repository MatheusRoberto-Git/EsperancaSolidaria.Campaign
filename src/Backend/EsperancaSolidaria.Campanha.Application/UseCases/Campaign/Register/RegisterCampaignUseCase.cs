using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Extensions;
using EsperancaSolidaria.Campanha.Domain.Repositories;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase;
using Mapster;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register
{
    public class RegisterCampaignUseCase : IRegisterCampaignUseCase
    {
        private readonly ICampaignWriteOnlyRepository _campaignWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCampaignUseCase(ICampaignWriteOnlyRepository campaignWriteOnlyRepository, IUnitOfWork unitOfWork)
        {
            _campaignWriteOnlyRepository = campaignWriteOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseRegisteredCampaignJson> Execute(RequestRegisterCampaignJson request)
        {
            // Normalizar a request
            NormalizeRequest(request);

            // Validar a request
            await Validate(request);

            // Mapear a request em uma entidade
            var campaign = request.Adapt<Domain.Entities.Campaign>();
            campaign.Status = Domain.Enums.CampaignStatus.Ativa;

            // Salvar no BD
            await _campaignWriteOnlyRepository.Add(campaign);
            await _unitOfWork.Commit();

            return new ResponseRegisteredCampaignJson
            {
                Title = campaign.Title
            };
        }

        private void NormalizeRequest(RequestRegisterCampaignJson request)
        {
            request.Title = request.Title.Trim();
            request.Description = request.Description.Trim();
            request.StartDate = request.StartDate.ToUniversalTime();
            request.EndDate = request.EndDate.ToUniversalTime();
        }

        private async Task Validate(RequestRegisterCampaignJson request)
        {
            var validator = new RegisterCampaignValidator();
            var result = await validator.ValidateAsync(request);

            if(result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}