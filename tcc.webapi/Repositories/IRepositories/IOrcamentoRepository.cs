using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IOrcamentoRepository : IGenericoRepository<Orcamento>
    {
        List<Orcamento> RecuperarTodosComOSGeradaPorPeriodo(OrcamentoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO);
        List<Orcamento> RecuperarTodosSemOSPorPeriodo(OrcamentoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO);
    }
}
