using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class ServicoRepository : GenericoRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }
    }
}
