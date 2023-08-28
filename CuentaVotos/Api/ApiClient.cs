﻿using CuentaVotos.Entiies.Shared;
using Microsoft.AspNetCore.Http.Json;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace CuentaVotos.Api
{
    public class ApiClient
    {
        public HttpClient Client { get; set; }
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
        public ApiClient(IConfiguration config)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(config.GetValue<string>("ApiSetting:UrlBase"));
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

            var res = JsonSerializer.Deserialize<ModelResult<T>>( await response.Content.ReadAsStringAsync(),jsonOptions); 
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

            var res = JsonSerializer.Deserialize<ModelResult<T>>(await response.Content.ReadAsStringAsync(), jsonOptions);
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

            var res = JsonSerializer.Deserialize<ModelResult<T>>(await response.Content.ReadAsStringAsync(), jsonOptions);
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

            var res = JsonSerializer.Deserialize<ModelResult<T>>(await response.Content.ReadAsStringAsync(), jsonOptions);
            return res;
        }

    }
}
