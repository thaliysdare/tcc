using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IClienteService
    {
        void Editar(int id, ClienteEnvio clienteEnvio);
        void Excluir(int id);
        ClienteRetorno Inserir(ClienteEnvio clienteEnvio);
        ClienteRetorno Recuperar(int id);
        List<ClienteRetorno> RecuperarTodos();
    }
}
