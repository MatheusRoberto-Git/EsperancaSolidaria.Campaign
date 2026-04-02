using System.Net;

namespace EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase
{
    public class NotFoundException : EsperancaSolidariaCampanhaException
    {
        public NotFoundException(string message) : base(message) { }

        public override IList<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}