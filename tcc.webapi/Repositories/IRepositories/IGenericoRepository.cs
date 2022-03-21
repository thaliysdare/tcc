using System;
using System.Linq;
using System.Linq.Expressions;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IGenericoRepository<TEntity>
        where TEntity : class, new()
    {
        IQueryable<TEntity> RecuperarTodos(params Expression<Func<TEntity, dynamic>>[] expressoes);
        TEntity RecuperarPorId(object id, params Expression<Func<TEntity, dynamic>>[] expressoes);
        void Inserir(TEntity entity);
        TEntity InserirERecuperar(TEntity entity);
        void Editar(TEntity entity);
        void Excluir(TEntity entity);
    }
}
