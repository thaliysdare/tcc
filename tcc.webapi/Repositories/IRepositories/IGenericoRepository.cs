using System.Linq;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IGenericoRepository<TEntity>
        where TEntity : class, new()
    {
        IQueryable<TEntity> RecuperarTodos();
        TEntity RecuperarPorId(int id);
        void Inserir(TEntity entity);
        TEntity InserirERecuperar(TEntity entity);
        void Editar(TEntity entity);
        void Excluir(TEntity entity);
    }
}
