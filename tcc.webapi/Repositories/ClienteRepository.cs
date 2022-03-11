using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class ClienteRepository : GenericoRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(BancoContexto bancoContexto) : base(bancoContexto) { }
    }
}
