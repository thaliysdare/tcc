using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;

namespace tcc.web.Services
{
    public class GenericoService
    {
        public readonly HttpClient _tccApi;

        public GenericoService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }

        public List<T> RecuperarTodos<T>(string endpoint, object envio = null)
        {
            HttpResponseMessage response;
            string json;

            if (envio != null)
            {
                json = JsonSerializer.Serialize(envio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = _tccApi.PostAsync(endpoint, content).Result;
            }
            else
            {
                response = _tccApi.GetAsync(endpoint).Result;
            }

            json = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonSerializer.Deserialize<Erro>(json);
                if (erro.status == 404) return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public T Recuperar<T>(string endpoint, int id)
        {
            var response = _tccApi.GetAsync($"{endpoint}/{id}").Result;
            var json = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonSerializer.Deserialize<Erro>(json);
                throw new Exception(erro.detail);
            }

            return JsonSerializer.Deserialize<T>(json);
        }

        public T Inserir<T>(string endpoint, object envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync(endpoint, content).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonSerializer.Deserialize<Erro>(json);
                throw new Exception(erro.detail);
            }
            return JsonSerializer.Deserialize<T>(json);
        }

        public void Editar(string endpoint, int id, object envio)
        {
            var json = JsonSerializer.Serialize(envio);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"{endpoint}/{id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonSerializer.Deserialize<Erro>(json);
                throw new Exception(erro.detail);
            }
        }

        public void Excluir(string endpoint, int id)
        {
            var response = _tccApi.DeleteAsync($"{endpoint}/{id}").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonSerializer.Deserialize<Erro>(json);
                throw new Exception(erro.detail);
            }
        }

    }
}
