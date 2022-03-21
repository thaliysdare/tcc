using System.Collections.Generic;
using tcc.webapi.Models;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IClienteRepository : IGenericoRepository<Cliente>
    {
        Cliente RecuperarClienteCompleto(int id);
        List<Cliente> RecuperarTodosClientesCompleto();
    }
}
