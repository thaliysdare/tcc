using System.Collections.Generic;
using tcc.webapi.Models;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IVeiculoRepository : IGenericoRepository<Veiculo>
    {
        List<Veiculo> RecuperarListaVeiculosPorCliente(int clienteId);
    }
}
