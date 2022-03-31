using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        public readonly HttpClient _tccApi;

        public OrdemServicoService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }


        public OrdemServicoRetorno Inserir(OrdemServicoEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync($"ordemservico", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<OrdemServicoRetorno>(json);
        }
        public OrdemServicoRetorno Editar(OrdemServicoEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"ordemservico/{envio.OrdemServicoId}", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<OrdemServicoRetorno>(json);
        }
        public OrdemServicoRetorno Recuperar(int id)
        {
            var json = _tccApi.GetStringAsync($"ordemservico/{id}").Result;
            return JsonSerializer.Deserialize<OrdemServicoRetorno>(json);
        }
        public List<OrdemServicoRetorno> RecuperarTodos()
        {
            var json = _tccApi.GetStringAsync($"ordemservico").Result;
            return JsonSerializer.Deserialize<List<OrdemServicoRetorno>>(json);
        }

        public void Excluir(int id)
        {
            var response = _tccApi.DeleteAsync($"ordemservico/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
