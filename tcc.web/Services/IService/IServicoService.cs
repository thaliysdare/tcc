using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IServicoService
    {
        void Editar(int id, ServicoEnvio clienteEnvio);
        ServicoRetorno Inserir(ServicoEnvio envio);
        ServicoRetorno Recuperar(int id);
        void Excluir(int id);
        List<ServicoRetorno> RecuperarTodos();
        List<ServicoRetorno> RecuperarApenasAtivos();
    }
}
