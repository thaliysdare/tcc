using tcc.webapi.Models;

namespace tcc.webapi.Services.IServices
{
    public interface IUsuarioService : IGenericoService
    {
        Usuario Inserir(Usuario model);
        void Editar(int id, Usuario model);
        void Inativar(int id);
    }
}
