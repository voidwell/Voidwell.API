using System.Net.Http;

namespace Voidwell.API.HttpDelegatedClient
{
    public class DelegatedHttpClientOptions
    {
        public HttpMessageHandler InnerMessageHandler { get; set; }
        public HttpMessageHandler TokenServiceMessageHandler { get; set; }
        public bool DisposeInnerMessageHandler { get; set; } = true;

        /// <summary>
        /// The number of milliseconds the token server has to respond before a timeout
        /// </summary>
        public int TokenServiceTimeout { get; set; } = 10000;

        /// <summary>
        /// The number of milliseconds a message has to be handled (including getting the access token) before a timeout
        /// </summary>
        public int MessageHandlerTimeout { get; set; } = 60000;
    }
}
