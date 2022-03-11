using System.Linq;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IGenericoRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> RecuperarTodos();
        void Inserir(TEntity entity);
        void Editar(TEntity entity);
        void Excluir(TEntity entity);
    }
}
