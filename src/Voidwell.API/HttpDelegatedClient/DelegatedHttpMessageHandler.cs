using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using IdentityModel.Client;
using Voidwell.API.HttpAuthenticatedClient;

namespace Voidwell.API.HttpDelegatedClient
{
    public class DelegatedHttpMessageHandler : HttpMessageHandler
    {
        private readonly DelegatedHttpClientOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenRetriever _tokenRetriever;
        private readonly ILogger _logger;

        private readonly string ApiScope = "voidwell-api";

        private readonly HttpMessageInvoker _httpMessageInvoker;
        private readonly TokenClient _tokenClient;

        public string TargetScope { get; set; }

        public DelegatedHttpMessageHandler(DelegatedHttpClientOptions options, IHttpContextAccessor httpContextAccessor,
            ITokenRetriever tokenRetriever, ILogger<DelegatedHttpMessageHandler> logger)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessor;
            _tokenRetriever = tokenRetriever;
            _logger = logger;

            _tokenClient = new TokenClient("http://voidwellauth:5000/connect/token",
                ApiScope,
                "apiSecret",
                _options.TokenServiceMessageHandler ?? new HttpClientHandler(),
                AuthenticationStyle.BasicAuthentication);

            var handler = new HttpClientHandler();
            _httpMessageInvoker = new HttpMessageInvoker(handler,
                _options.InnerMessageHandler == null || _options.DisposeInnerMessageHandler);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken callerCancellationToken)
        {
            CancellationTokenSource messageHandlerTimeoutCancellationTokenSource =
                new CancellationTokenSource(_options.MessageHandlerTimeout);
            CancellationToken httpCancellationToken =
                _httpContextAccessor?.HttpContext?.RequestAborted ?? CancellationToken.None;
            CancellationTokenSource linkedCancelationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                callerCancellationToken, messageHandlerTimeoutCancellationTokenSource.Token, httpCancellationToken);

            try
            {
                return await GetTokenAndSendAsync(request, linkedCancelationTokenSource.Token);
            }
            catch (TokenServiceResponseException ex)
            when (messageHandlerTimeoutCancellationTokenSource.Token.IsCancellationRequested && ex.TokenResponse.Exception is OperationCanceledException)
            {
                // When the cancelation happens within the token service, the exception is wrapped
                //Only catches when messageHandlerTimeoutCancellationTokenSource is canceled, not when caller cancels
                var msg = string.Format("{0} exceeded the timed out of {1} ms when making a request",
                    nameof(AuthenticatedHttpMessageHandler), _options.MessageHandlerTimeout);

                _logger.LogError(0, ex, msg);

                throw new TimeoutException(msg, ex);
            }
            catch (OperationCanceledException ex) when (messageHandlerTimeoutCancellationTokenSource.Token.IsCancellationRequested)
            {
                //Only catches when messageHandlerTimeoutCancellationTokenSource is canceled, not when caller cancels
                var msg = string.Format("{0} exceeded the timed out of {1} ms when making a request",
                    nameof(AuthenticatedHttpMessageHandler), _options.MessageHandlerTimeout);

                _logger.LogError(0, ex, msg);

                throw new TimeoutException(msg, ex);
            }
        }

        private async Task<HttpResponseMessage> GetTokenAndSendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CancellationTokenSource tokenServiceTimeoutCancellationTokenSource = null;

            try
            {
                // Starts timer on local cancellation source
                tokenServiceTimeoutCancellationTokenSource = new CancellationTokenSource(_options.TokenServiceTimeout);

                // Creates a cancelation source 
                CancellationTokenSource linkedCancellationTokenSource = CancellationTokenSource
                    .CreateLinkedTokenSource(cancellationToken, tokenServiceTimeoutCancellationTokenSource.Token);

                var requestToken = await _tokenRetriever.GetRequestToken();

                var payload = new
                {
                    token = requestToken
                };

                var response = await _tokenClient.RequestCustomGrantAsync("delegation", TargetScope, payload,
                    cancellationToken: linkedCancellationTokenSource.Token);

                ValidateTokenResponse(response);

                var delegateToken = response.AccessToken;

                _logger.LogInformation("Delegate token found for client '{0}' with scope '{1}'.", ApiScope, TargetScope);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", delegateToken);
                return await _httpMessageInvoker.SendAsync(request, cancellationToken);
            }
            catch (TokenServiceResponseException ex) when (tokenServiceTimeoutCancellationTokenSource.Token.IsCancellationRequested)
            {
                //Only catches when tokenServiceTimeoutCancellationTokenSource is canceled, not when caller cancels
                string msg = string.Format("{0} exceeded the time out of {1} ms when attempting to call token service.",
                    nameof(AuthenticatedHttpMessageHandler), _options.TokenServiceTimeout);

                _logger.LogError(0, ex, msg);

                throw new TimeoutException(msg, ex);
            }
        }

        private void ValidateTokenResponse(TokenResponse response)
        {
            if (response.IsError)
            {
                var msg = $"The token service had an error.\r\nClient ID: {ApiScope}\r\nStatusCode: {response.HttpStatusCode}\r\nHttpErrorReason: {response.HttpErrorReason}";
                msg += $"Error {response.Error}\r\nError Description: {response.ErrorDescription}\r\nError Type: {response.ErrorType}";

                throw new TokenServiceResponseException(response, msg);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
                _httpMessageInvoker.Dispose();
                _tokenClient.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
