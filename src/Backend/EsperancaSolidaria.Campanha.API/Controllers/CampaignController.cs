using EsperancaSolidaria.Campanha.Application.UseCases.Campaign.Register;
using EsperancaSolidaria.Campanha.Communication.Requests;
using EsperancaSolidaria.Campanha.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EsperancaSolidaria.Campanha.API.Controllers
{    
    public class CampaignController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCampaignJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromServices] IRegisterCampaignUseCase useCase, [FromBody] RequestRegisterCampaignJson request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}