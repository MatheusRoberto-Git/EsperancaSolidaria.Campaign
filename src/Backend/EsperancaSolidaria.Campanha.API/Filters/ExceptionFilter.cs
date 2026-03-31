using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Exceptions;
using EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EsperancaSolidaria.Campanha.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is EsperancaSolidariaCampanhaException esperancaSolidariaCampanhaException)
            {
                HandleProjectException(esperancaSolidariaCampanhaException, context);
            }
            else
            {
                ThrowUnknowException(context);
            }
        }        

        private static void HandleProjectException(EsperancaSolidariaCampanhaException esperancaSolidariaCampanhaException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)esperancaSolidariaCampanhaException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(esperancaSolidariaCampanhaException.GetErrorMessages()));
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        }
    }
}