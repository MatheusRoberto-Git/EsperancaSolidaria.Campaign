using EsperancaSolidaria.Campanha.Communication.Responses;
using EsperancaSolidaria.Campanha.Domain.Security.Tokens;
using EsperancaSolidaria.Campanha.Exceptions;
using EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace EsperancaSolidaria.Campanha.API.Filters
{
    public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
    {
        private readonly IAccessTokenValidator _accessTokenValidator;

        public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator)
        {
            _accessTokenValidator = accessTokenValidator;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context);

                _accessTokenValidator.ValidateAndGetUserIdentifier(token);
            }
            catch(SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.TOKEN_EXPIRED)
                {
                    TokenExpired = true
                });
            }
            catch
            {
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
            }
        }

        private static string TokenOnRequest(AuthorizationFilterContext context)
        {
            var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

            if(string.IsNullOrEmpty(authentication))
            {
                throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
            }

            return authentication["Bearer ".Length..].Trim();
        }
    }
}