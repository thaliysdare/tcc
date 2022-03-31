using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class ServicoService : IServicoService
    {
        public readonly HttpClient _tccApi;

        public ServicoService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }


        public ServicoRetorno Inserir(ServicoEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync($"servicos", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<ServicoRetorno>(json);
        }
        public ServicoRetorno Editar(ServicoEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"servicos/{envio.ServicoId}", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<ServicoRetorno>(json);
        }
        public ServicoRetorno Recuperar(int id)
        {
            var json = _tccApi.GetStringAsync($"servicos/{id}").Result;
            return JsonSerializer.Deserialize<ServicoRetorno>(json);
        }
        public List<ServicoRetorno> RecuperarTodos()
        {
            var json = _tccApi.GetStringAsync($"servicos").Result;
            return JsonSerializer.Deserialize<List<ServicoRetorno>>(json);
        }
        public List<ServicoRetorno> RecuperarApenasAtivos()
        {
            var json = _tccApi.GetStringAsync($"servicos").Result;
            return JsonSerializer.Deserialize<List<ServicoRetorno>>(json).Where(x => x.Ativo).ToList();
        }

        public void Excluir(int id)
        {
            var response = _tccApi.DeleteAsync($"servicos/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
