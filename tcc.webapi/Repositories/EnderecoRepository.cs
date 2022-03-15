using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class EnderecoRepository : GenericoRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(BancoContexto bancoContexto) : base(bancoContexto) { }
    }
}
