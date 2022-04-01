using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class ServicoService : IServicoService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public ServicoService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<ServicoRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<ServicoRetorno>("servicos");
        }

        public ServicoRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<ServicoRetorno>("servicos", id);
        }

        public ServicoRetorno Inserir(ServicoEnvio clienteEnvio)
        {
            return genericoService.Inserir<ServicoRetorno>("servicos", clienteEnvio);
        }

        public void Editar(int id, ServicoEnvio clienteEnvio)
        {
            genericoService.Editar("servicos", id, clienteEnvio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("servicos", id);
        }

        public List<ServicoRetorno> RecuperarApenasAtivos()
        {
            return RecuperarTodos().Where(x => x.Ativo).ToList();
        }
    }
}
