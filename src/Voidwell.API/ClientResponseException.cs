using System;

namespace Voidwell.API
{
    public class ClientResponseException : Exception
    {
        public ClientResponseException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ClientResponseException(int statusCode, string content) : this(statusCode)
        {
            Content = content;
        }

        public int StatusCode { get; }
        public string Content { get; }
    }
}
