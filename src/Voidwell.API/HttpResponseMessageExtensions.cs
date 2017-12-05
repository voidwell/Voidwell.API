﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Voidwell.API
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> GetContentAsync<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = null;

                try
                {
                    errorContent = await response.Content?.ReadAsStringAsync();
                }
                catch(Exception ex)
                {
                    errorContent = $"Inner Error when attempting to read error content. Unable to get actual error '{ex}'";
                }

                throw new ClientResponseException((int)response.StatusCode, errorContent);
            }

            return await response.Content.ReadAsObjectAsync<T>();
        }
    }
}
