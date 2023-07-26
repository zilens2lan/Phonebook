using System.Net;

namespace Phonebook.Exceptions
{
    public class HttpResponseException : Exception
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _description;
        public HttpResponseException(HttpStatusCode statusCode, string description) 
        {
            _statusCode = statusCode;
            _description = description;
        }
    }
}
