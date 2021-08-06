using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core
{
    public class BaseClient
    {
        protected HttpClient HttpClient { get; }
        public BaseClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        protected async Task<T> PostAsync<T>(object obj, string path, string mediaType = "application/json")
        {
            using StringContent stringContent = new StringContent(JsonSerializer.Serialize(obj),
                                                                  Encoding.UTF8, mediaType);
            using HttpResponseMessage response = await HttpClient.PostAsync(path, stringContent);
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        }

        protected async Task<T> GetAsync<T>(string path, string mediaType = "application/json")
        {
            using HttpResponseMessage response = await HttpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }

            return default;
        }
    }
}
