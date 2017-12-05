using IdentityModel.Client;
using System;

namespace Voidwell.API
{
    public class TokenServiceResponseException : Exception
    {
        public TokenServiceResponseException(TokenResponse tokenResponse, string message)
            : base(message, tokenResponse.Exception)
        {
            TokenResponse = tokenResponse;
        }

        public TokenResponse TokenResponse { get; }
    }
}
