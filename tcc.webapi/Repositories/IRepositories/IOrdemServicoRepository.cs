using System.Collections.Generic;
using tcc.webapi.Models;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IOrdemServicoRepository : IGenericoRepository<OrdemServico>
    {
        List<OrdemServico> RecuperarTodosOrdemServicoCompleto();
        OrdemServico RecuperarOrdemServicoCompleto(int id);
    }
}
