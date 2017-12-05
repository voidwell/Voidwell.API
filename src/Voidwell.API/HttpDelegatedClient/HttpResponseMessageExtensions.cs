using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace Voidwell.API.HttpDelegatedClient
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetResponseContentAsync<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = null;

                try
                {
                    errorContent = await response.Content?.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    // probably wont't ever happen
                    errorContent = $"Inner Error when attempting to read error content. Unable to get actual error '{ex}'";
                }

                throw new ClientResponseException((int)response.StatusCode, errorContent);
            }

            var serializedString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedString);
        }
    }
}
