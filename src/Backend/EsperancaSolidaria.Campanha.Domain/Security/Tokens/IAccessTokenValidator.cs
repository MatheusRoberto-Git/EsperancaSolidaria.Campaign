using EsperancaSolidaria.Campanha.Domain.Enums;

namespace EsperancaSolidaria.Campanha.Domain.Security.Tokens
{
    public interface IAccessTokenValidator
    {
        public Guid ValidateAndGetUserIdentifier(string token);
        public UserRole ValidateAndGetUserRole(string token);

    }
}