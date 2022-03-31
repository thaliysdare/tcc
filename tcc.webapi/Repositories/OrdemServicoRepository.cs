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

    }
}
