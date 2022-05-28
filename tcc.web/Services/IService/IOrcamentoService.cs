using System;
using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IOrcamentoService
    {
        void Editar(int id, OrcamentoEnvio ordemServicoEnvio);
        OrcamentoRetorno Inserir(OrcamentoEnvio envio);
        OrcamentoRetorno Recuperar(int id);
        List<OrcamentoRetorno> RecuperarTodos();
        List<OrcamentoRetorno> RecuperarTodosComOSGeradaPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        List<OrcamentoRetorno> RecuperarTodosSemOSPorPeriodo(DateTime dataInicial, DateTime dataFinal);
    }
}
