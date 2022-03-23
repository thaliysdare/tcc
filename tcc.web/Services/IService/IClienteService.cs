using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc.web.Models.DTO;

namespace tcc.web.Services.IService
{
    public interface IClienteService
    {
        Task EditarCliente(int id, ClienteEnvio clienteEnvio);
        Task ExcluirCliente(ClienteEnvio clienteEnvio);
        Task<ClienteRetorno> InserirCliente(ClienteEnvio clienteEnvio);
        Task<List<ClienteRetorno>> RecuperarClientes();
    }
}
