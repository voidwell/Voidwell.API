using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Voidwell.API
{
    public class TokenRetriever : ITokenRetriever
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        const string _accessTokeKey = "access_token";
        private readonly ILogger _logger;

        public TokenRetriever(IHttpContextAccessor httpContextAccessor, ILogger<TokenRetriever> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public Task<string> GetRequestToken()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            var token = TokenRetrieval.FromAuthorizationHeader()(request);

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogError("Token not found in header");
            }
            else
            {
                _logger.LogInformation("Token retrieved from header");
            }

            return Task.FromResult(token);
        }
    }
}
