using System;
using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class OrcamentoRepository : GenericoRepository<Orcamento>, IOrcamentoRepository
    {
        public OrcamentoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }

    }
}
