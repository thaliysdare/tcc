using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tcc.web.Models.DTO;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClienteService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<List<ClienteRetorno>> RecuperarClientes()
        {
            var json = _httpClientFactory.CreateClient("tcc.api").GetStringAsync("clientes").Result;
            var listaClientes = JsonSerializer.Deserialize<List<ClienteRetorno>>(json);
            return listaClientes.Where(x => x.Ativo).ToList();
        }

        public async Task<ClienteRetorno> InserirCliente(ClienteEnvio clienteEnvio)
        {
            var jsonEnvio = JsonSerializer.Serialize(clienteEnvio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _httpClientFactory.CreateClient("tcc.api").PostAsync("clientes", content).Result;
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ClienteRetorno>(json);
        }

        public async Task EditarCliente(int id, ClienteEnvio clienteEnvio)
        {
            var content = new StringContent(JsonSerializer.Serialize(clienteEnvio), Encoding.UTF8, "application/json");
            var response = _httpClientFactory.CreateClient("tcc.api").PutAsync($"clientes/{id}", content).Result;
            response.EnsureSuccessStatusCode();
        }

        public async Task ExcluirCliente(ClienteEnvio clienteEnvio)
        {
            //var content = new StringContent(JsonSerializer.Serialize(clienteEnvio), Encoding.UTF8, "application/json");
            //var response = _httpClientFactory.CreateClient("tcc.api").DeleteAsync("clientes", content).Result;
            //response.EnsureSuccessStatusCode();
        }

        
    }
}
