using EsperancaSolidaria.Campanha.Domain.Enums;
using EsperancaSolidaria.Campanha.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EsperancaSolidaria.Campanha.Infrastructure.Security.Tokens.Access.Validator
{
    public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
    {
        private readonly string _signingKey;

        public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

        public Guid ValidateAndGetUserIdentifier(string token)
        {
            var principal = Validate(token);
            var userIdentifier = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            return Guid.Parse(userIdentifier);
        }

        public UserRole ValidateAndGetUserRole(string token)
        {
            var principal = Validate(token);
            var role = principal.Claims.First(c => c.Type == ClaimTypes.Role).Value;
            return Enum.Parse<UserRole>(role);
        }

        private ClaimsPrincipal Validate(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = SecurityKey(_signingKey),
                ClockSkew = new TimeSpan(0)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
    }
}