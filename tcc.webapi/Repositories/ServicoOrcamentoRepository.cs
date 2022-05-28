using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class ServicoOrcamentoRepository : GenericoRepository<ServicoOrcamento>, IServicoOrcamentoRepository
    {
        public ServicoOrcamentoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }
    }
}
