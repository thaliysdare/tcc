using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IUsuarioService
    {
        void Editar(int id, UsuarioEnvio clienteEnvio);
        UsuarioRetorno Inserir(UsuarioEnvio envio);
        UsuarioRetorno Recuperar(int id);
        void Excluir(int id);
        List<UsuarioRetorno> RecuperarTodos();
        UsuarioRetorno ValidarAutenticacao(UsuarioAutenticacao usuario);
    }
}
