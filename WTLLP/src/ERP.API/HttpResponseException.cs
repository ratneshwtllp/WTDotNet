using System;
using System.Net.Http;

namespace ERP.Api
{
    internal class HttpResponseException : Exception
    {
        private HttpResponseMessage httpResponseMessage;

        public HttpResponseException()
        {
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(HttpResponseMessage httpResponseMessage)
        {
            this.httpResponseMessage = httpResponseMessage;
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}