using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class ClienteService : IClienteService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public ClienteService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<ClienteRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<ClienteRetorno>("clientes");
        }

        public ClienteRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<ClienteRetorno>("clientes", id);
        }

        public ClienteRetorno Inserir(ClienteEnvio clienteEnvio)
        {
            return genericoService.Inserir<ClienteRetorno>("clientes", clienteEnvio);
        }

        public void Editar(int id, ClienteEnvio clienteEnvio)
        {
            genericoService.Editar("clientes", id, clienteEnvio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("clientes", id);
        }

    }
}
