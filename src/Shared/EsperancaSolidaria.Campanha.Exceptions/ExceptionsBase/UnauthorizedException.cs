using System.Net;

namespace EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase
{
    public class UnauthorizedException : EsperancaSolidariaCampanhaException
    {
        public UnauthorizedException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}