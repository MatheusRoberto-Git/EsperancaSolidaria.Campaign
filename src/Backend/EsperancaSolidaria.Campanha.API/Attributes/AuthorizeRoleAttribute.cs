using EsperancaSolidaria.Campanha.API.Filters;
using EsperancaSolidaria.Campanha.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EsperancaSolidaria.Campanha.API.Attributes
{
    public class AuthorizeRoleAttribute : TypeFilterAttribute
    {
        public AuthorizeRoleAttribute(params UserRole[] roles) : base(typeof(AuthorizeRoleFilter))
        {
            Arguments = [roles];
        }
    }
}