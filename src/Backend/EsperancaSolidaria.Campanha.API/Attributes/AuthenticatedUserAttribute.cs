using EsperancaSolidaria.Campanha.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EsperancaSolidaria.Campanha.API.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) { }
    }
}