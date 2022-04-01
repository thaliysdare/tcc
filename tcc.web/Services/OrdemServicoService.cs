using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public OrdemServicoService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<OrdemServicoRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<OrdemServicoRetorno>("ordemservico");
        }

        public OrdemServicoRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<OrdemServicoRetorno>("ordemservico", id);
        }

        public OrdemServicoRetorno Inserir(OrdemServicoEnvio ordemServicoEnvio)
        {
            return genericoService.Inserir<OrdemServicoRetorno>("ordemservico", ordemServicoEnvio);
        }

        public void Editar(int id, OrdemServicoEnvio ordemServicoEnvio)
        {
            genericoService.Editar("ordemservico", id, ordemServicoEnvio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("ordemservico", id);
        }
    }
}
