using System.Net;

namespace EsperancaSolidaria.Campanha.Exceptions.ExceptionsBase
{
    public abstract class EsperancaSolidariaCampanhaException : SystemException
    {
        protected EsperancaSolidariaCampanhaException(string message) : base(message) { }

        public abstract IList<string> GetErrorMessages();

        public abstract HttpStatusCode GetStatusCode();
    }
}