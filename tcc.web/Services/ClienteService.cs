using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class ClienteService : IClienteService
    {
        public readonly HttpClient _tccApi;

        public ClienteService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }

        public List<ClienteRetorno> RecuperarTodos()
        {
            var json = _tccApi.GetStringAsync("clientes").Result;
            var listaClientes = JsonSerializer.Deserialize<List<ClienteRetorno>>(json);
            return listaClientes.ToList();
        }

        public ClienteRetorno Recuperar(int id)
        {
            var json = _tccApi.GetStringAsync($"clientes/{id}").Result;
            return JsonSerializer.Deserialize<ClienteRetorno>(json);
        }

        public ClienteRetorno Inserir(ClienteEnvio clienteEnvio)
        {
            var jsonEnvio = JsonSerializer.Serialize(clienteEnvio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync("clientes", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<ClienteRetorno>(json);
        }

        public void Editar(int id, ClienteEnvio clienteEnvio)
        {
            var json = JsonSerializer.Serialize(clienteEnvio);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"clientes/{id}", content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void Excluir(int id)
        {
            var response = _tccApi.DeleteAsync($"clientes/{id}").Result;
            response.EnsureSuccessStatusCode();
        }

    }
}
