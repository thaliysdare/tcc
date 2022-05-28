using System;
using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public OrcamentoService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public List<OrcamentoRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<OrcamentoRetorno>("orcamento");
        }

        public List<OrcamentoRetorno> RecuperarTodosComOSGeradaPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return genericoService.RecuperarTodos<OrcamentoRetorno>("orcamento/osgerada/periodo", new
            {
                dataInicial,
                dataFinal
            });
        }

        public List<OrcamentoRetorno> RecuperarTodosSemOSPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return genericoService.RecuperarTodos<OrcamentoRetorno>("orcamento/semos/periodo", new
            {
                dataInicial,
                dataFinal
            });
        }

        public OrcamentoRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<OrcamentoRetorno>("orcamento", id);
        }

        public OrcamentoRetorno Inserir(OrcamentoEnvio orcamentoEnvio)
        {
            return genericoService.Inserir<OrcamentoRetorno>("orcamento", orcamentoEnvio);
        }

        public void Editar(int id, OrcamentoEnvio orcamentoEnvio)
        {
            genericoService.Editar("orcamento", id, orcamentoEnvio);
        }
    }
}
