using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class ServicoOrdemServicoRepository : GenericoRepository<ServicoOrdemServico>, IServicoOrdemServicoRepository
    {
        public ServicoOrdemServicoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }
    }
}
