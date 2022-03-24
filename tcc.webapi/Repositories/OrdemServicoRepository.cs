using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class OrdemServicoRepository : GenericoRepository<OrdemServico>, IOrdemServicoRepository
    {
        public OrdemServicoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }

        public OrdemServico RecuperarOrdemServicoCompleto(int id)
        {
            return RecuperarPorId(id,
                                  x => x.Usuario,
                                  x => x.ServicoOrdemServico,
                                  x => x.Cliente,
                                  x => x.Veiculo);
        }

        public List<OrdemServico> RecuperarTodosOrdemServicoCompleto()
        {
            return RecuperarTodos(x => x.Usuario,
                                  x => x.ServicoOrdemServico,
                                  x => x.Cliente,
                                  x => x.Veiculo)
                   .ToList();
        }
    }
}
