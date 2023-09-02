
using CuentaVotos.Entities.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace Elecciones.Client.Application
{
    public class ApiClient
    {
        public HttpClient Client { get; set; }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public ApiClient(IConfiguration config, HttpClient httpClient)
        {
            Client = httpClient;
            //Client.BaseAddress = new Uri(config.GetValue<string>("ApiSetting:UrlBase"));
        }

        public void SetAuthorization(string token)
        {
            RemoveAuthorization();
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public void RemoveAuthorization()
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
        }

        public async Task<ModelResult<T>> GetAsync<T>(string url)
        {
            var response = await Client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new ModelResult<T>
                {
                    IsSuccess = false,
                    Message = "Error al conectar con el servicio"
                };
            }

            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<ModelResult<T>>(content, jsonOptions);
            return res;
        }

        public async Task<ModelResult<T>> PostAsync<T, K>(string url, K model)
        {
            var response = await Client.PostAsJsonAsync(url, model);
            if (!response.IsSuccessStatusCode)
            {
                return new ModelResult<T>
                {
                    IsSuccess = false,
                    Message = "Error al conectar con el servicio"
                };
            }
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<ModelResult<T>>(content, jsonOptions);
            return res;
        }

        public async Task<ModelResult<T>> PutAsync<T, K>(string url, K model)
        {
            var response = await Client.PutAsJsonAsync(url, model);
            if (!response.IsSuccessStatusCode)
            {
                return new ModelResult<T>
                {
                    IsSuccess = false,
                    Message = "Error al conectar con el servicio"
                };
            }
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<ModelResult<T>>(content, jsonOptions);
            return res;
        }

        public async Task<ModelResult<T>> DeleteAsync<T>(string url)
        {
            var response = await Client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new ModelResult<T>
                {
                    IsSuccess = false,
                    Message = "Error al conectar con el servicio"
                };
            }
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<ModelResult<T>>(content, jsonOptions);
            return res;
        }

    }
}
