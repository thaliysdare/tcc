using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class VeiculoService : IVeiculoService
    {
        public readonly HttpClient _tccApi;

        public VeiculoService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }


        public VeiculoRetorno Inserir(VeiculoEnvio veiculoEnvio)
        {
            var jsonEnvio = JsonSerializer.Serialize(veiculoEnvio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync($"veiculos", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<VeiculoRetorno>(json);
        }
        public VeiculoRetorno Editar(VeiculoEnvio veiculoEnvio)
        {
            var jsonEnvio = JsonSerializer.Serialize(veiculoEnvio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"veiculos/{veiculoEnvio.VeiculoId}", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<VeiculoRetorno>(json);
        }
        public VeiculoRetorno Recuperar(int veiculoId)
        {
            var json = _tccApi.GetStringAsync($"veiculos/{veiculoId}").Result;
            return JsonSerializer.Deserialize<VeiculoRetorno>(json);
        }
        public List<VeiculoRetorno> RecuperarTodos()
        {
            var json = _tccApi.GetStringAsync($"veiculos").Result;
            return JsonSerializer.Deserialize<List<VeiculoRetorno>>(json);
        }
    }
}
