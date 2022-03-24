using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class UsuarioRepository : GenericoRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }
    }
}
