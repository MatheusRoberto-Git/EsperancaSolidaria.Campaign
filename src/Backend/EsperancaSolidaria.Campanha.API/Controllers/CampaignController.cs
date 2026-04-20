using EsperancaSolidaria.Campanha.API.Attributes;
using EsperancaSolidaria.Campanha.API.Binders;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Admin;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Get.Public;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register;
using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Update;
using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EsperancaSolidaria.Campanha.API.Controllers
{
    public class CampaignController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCampaignJson), StatusCodes.Status201Created)]
        [AuthenticatedUser]
        [AuthorizeRole(UserRole.GestorONG)]
        public async Task<IActionResult> Register([FromServices] IRegisterCampaignUseCase useCase, [FromBody] RequestRegisterCampaignJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [AuthenticatedUser]
        [AuthorizeRole(UserRole.GestorONG)]
        public async Task<IActionResult> Update([FromServices] IUpdateCampaignUseCase useCase, [FromRoute][ModelBinder(typeof(EsperancaSolidariaCampaignBinder))] long id, [FromBody] RequestUpdateCampaignJson request)
        {
            await useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseCampaignsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IGetCampaignsUseCase useCase)
        {
            var response = await useCase.Execute();

            if(response.Campaigns.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }

        [HttpGet("campanha-gestor")]
        [ProducesResponseType(typeof(ResponseCampaignsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AuthenticatedUser]
        [AuthorizeRole(UserRole.GestorONG)]
        public async Task<IActionResult> GetGestor([FromServices] IGetCampaignsAdminUseCase useCase)
        {
            var response = await useCase.Execute();

            if(response.Campaigns.Any())
            {
                return Ok(response);
            }

            return NoContent();
        }
    }
}