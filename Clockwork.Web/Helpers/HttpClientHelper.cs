using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Clockwork.Web.Helpers
{
    public static class HttpClientHelper
    {
        private static HttpClient restClient = new HttpClient();

        public static async Task<T> GetItemAsync<T>(string apiUrl)
        {
            var result = default(T);
            var response = await restClient.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(content);
            }

            return result;
        }

        public static async Task<List<T>> GetItemsAsync<T>(string apiUrl)
        {
            var result = new List<T>();
            var response = await restClient.GetAsync(apiUrl).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<T>>(content);
            }
            return result;
        }

        public static async Task<T> PostRequest<T>(string apiUrl, dynamic postBody)
        {
            var result = default(T);
            var jsonBody = JsonConvert.SerializeObject(postBody);

            var response = await restClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(content);
            }
            return result;
        }
    }
}