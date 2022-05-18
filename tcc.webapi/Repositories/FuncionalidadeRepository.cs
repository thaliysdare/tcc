using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class FuncionalidadeRepository : GenericoRepository<Funcionalidade>, IFuncionalidadeRepository
    {
        public FuncionalidadeRepository(BancoContexto bancoContexto) : base(bancoContexto) { }

    }
}
