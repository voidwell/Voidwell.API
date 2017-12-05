using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Voidwell.API
{
    public class ClientResponseExceptionFilter : IExceptionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public ClientResponseExceptionFilter(IHttpContextAccessor httpContextAccessor,
            ILogger<ClientResponseExceptionFilter> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ClientResponseException)
            {
                var clientResponseException = context.Exception as ClientResponseException;

                if (clientResponseException.StatusCode != 404)
                {
                    LogException(clientResponseException);
                }

                context.Result = new ContentResult
                {
                    StatusCode = (int)clientResponseException.StatusCode,
                    Content = clientResponseException.Content,
                    ContentType = "text/plain"
                };
            }
        }

        private void LogException(ClientResponseException ex)
        {
            var logMessage = "Error when attempting to hit path {0}. Status code {1}. Response Content {2}";
            object[] parameters = new object[3];
            parameters[0] = _httpContextAccessor.HttpContext.Request.Path;
            parameters[1] = ex.StatusCode;
            parameters[2] = ex.Content;

            if ((ex.StatusCode >= 400 && ex.StatusCode <= 403)
                || ex.StatusCode == 409)
            {
                _logger.LogWarning(logMessage, parameters);
            }
            else
            {
                _logger.LogError(logMessage, parameters);
            }
        }
    }
}
