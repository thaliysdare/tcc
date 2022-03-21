using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class ClienteRepository : GenericoRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(BancoContexto bancoContexto) : base(bancoContexto) { }

        public Cliente RecuperarClienteCompleto(int id)
        {
            return RecuperarPorId(id,
                                  x => x.Endereco,
                                  x => x.Veiculo);
        }

        public List<Cliente> RecuperarTodosClientesCompleto()
        {
            return RecuperarTodos(x => x.Endereco,
                                  x => x.Veiculo)
                   .ToList();
        }
    }
}
