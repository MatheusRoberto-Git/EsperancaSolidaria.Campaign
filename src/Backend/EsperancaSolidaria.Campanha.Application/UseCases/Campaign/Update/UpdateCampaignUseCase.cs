using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Domain.Repositories;
using EsperancaSolidaria.Campanha.Domain.Repositories.Campaign;
using EsperancaSolidaria.Campanha.Exceptions;
using EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase;
using Mapster;

namespace EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Update
{
    public class UpdateCampaignUseCase : IUpdateCampaignUseCase
    {
        private readonly ICampaignUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCampaignUseCase(ICampaignUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long campaignId, RequestUpdateCampaignJson request)
        {
            //Normalizar a request
            NormalizeRequest(request);

            // Buscar a entidade no BD
            var campaign = await _repository.GetById(campaignId);

            if(campaign is null)
            {
                throw new NotFoundException(ResourceMessagesException.CAMPAIGN_NOT_FOUND);
            }

            // Validar se a campanha pode ser editada (status != Ativa)
            if(campaign.Status != Domain.Enums.CampaignStatus.Ativa)
            {
                throw new ErrorOnValidationException([ResourceMessagesException.CAMPAIGN_ALREADY_CLOSED]);
            }

            // Validar a request
            Validate(request);

            // Mapear a request em uma entidade
            var mapper = request.Adapt<Domain.Entities.Campaign>();

            // Salvar no BD
            _repository.Update(mapper);
            await _unitOfWork.Commit();
        }

        private void NormalizeRequest(RequestUpdateCampaignJson request)
        {
            request.Title = request.Title.Trim();
            request.Description = request.Description.Trim();
            request.StartDate = request.StartDate.ToUniversalTime();
            request.EndDate = request.EndDate.ToUniversalTime();
        }

        private static void Validate(RequestUpdateCampaignJson request)
        {
            var result = new UpdateCampaignValidator().Validate(request);

            if(result.IsValid is false)
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}