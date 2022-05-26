using System;
using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IOrdemServicoService
    {
        void Editar(int id, OrdemServicoEnvio ordemServicoEnvio);
        OrdemServicoRetorno Inserir(OrdemServicoEnvio envio);
        OrdemServicoRetorno Recuperar(int id);
        void Excluir(int id);
        List<OrdemServicoRetorno> RecuperarTodos();
        List<OrdemServicoRetorno> RecuperarTodosFinalizadosPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        List<OrdemServicoRetorno> RecuperarTodosCanceladosPorPeriodo(DateTime dataInicial, DateTime dataFinal);
    }
}
