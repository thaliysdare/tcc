using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IUsuarioService
    {
        UsuarioRetorno Editar(UsuarioEnvio envio);
        UsuarioRetorno Inserir(UsuarioEnvio envio);
        UsuarioRetorno Recuperar(int id);
        void Excluir(int id);
        List<UsuarioRetorno> RecuperarTodos();
        List<UsuarioRetorno> RecuperarApenasAtivos();
        UsuarioRetorno ValidarAutenticacao(UsuarioAutenticacao usuario);
    }
}
