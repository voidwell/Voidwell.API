using System.Net.Http;
using System.Threading.Tasks;

namespace Voidwell.API
{
    public static class HttpResponseMessageExtensions
    {
        public static Task<T> GetContentAsync<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsObjectAsync<T>();
        }
    }
}
