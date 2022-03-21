using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class VeiculoRepository : GenericoRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(BancoContexto bancoContexto) : base(bancoContexto) { }

        public List<Veiculo> RecuperarListaVeiculosPorCliente(int clienteId)
        {
            return RecuperarTodos()
                   .Where(x => x.ClienteId == clienteId)
                   .ToList();
        }
    }
}
