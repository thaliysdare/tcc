using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class FuncionalidadeService : IFuncionalidadeService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public FuncionalidadeService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<FuncionalidadeRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<FuncionalidadeRetorno>("funcionalidades");
        }

        public FuncionalidadeRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<FuncionalidadeRetorno>("funcionalidades", id);
        }

        public FuncionalidadeRetorno Inserir(FuncionalidadeEnvio envio)
        {
            return genericoService.Inserir<FuncionalidadeRetorno>("funcionalidades", envio);
        }

        public void Editar(int id, FuncionalidadeEnvio envio)
        {
            genericoService.Editar("funcionalidades", id, envio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("funcionalidades", id);
        }
    }
}
