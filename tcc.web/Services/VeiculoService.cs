using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class VeiculoService : IVeiculoService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public VeiculoService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<VeiculoRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<VeiculoRetorno>("veiculos");
        }

        public VeiculoRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<VeiculoRetorno>("veiculos", id);
        }

        public VeiculoRetorno Inserir(VeiculoEnvio clienteEnvio)
        {
            return genericoService.Inserir<VeiculoRetorno>("veiculos", clienteEnvio);
        }

        public void Editar(int id, VeiculoEnvio clienteEnvio)
        {
            genericoService.Editar("veiculos", id, clienteEnvio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("veiculos", id);
        }
    }
}
