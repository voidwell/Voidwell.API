using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Voidwell.API
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent content)
        {
            var serializedString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedString);
        }
    }
}
