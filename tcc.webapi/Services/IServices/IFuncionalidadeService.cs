using tcc.webapi.Models;

namespace tcc.webapi.Services.IServices
{
    public interface IFuncionalidadeService : IGenericoService
    {
        Funcionalidade Inserir(Funcionalidade model);
        void Editar(int id, Funcionalidade model);
    }
}
