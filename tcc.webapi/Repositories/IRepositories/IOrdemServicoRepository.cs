using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IOrdemServicoRepository : IGenericoRepository<OrdemServico>
    {
        List<OrdemServico> RecuperarTodosCanceladosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO);
        List<OrdemServico> RecuperarTodosFinalizadosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO);
        List<OrdemServico> RecuperarTodosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO);
    }
}
