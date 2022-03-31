using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class GenericoRepository<TEntity> : IGenericoRepository<TEntity>
        where TEntity : class, new()
    {
        public readonly BancoContexto _bancoContexto;

        public GenericoRepository(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        public IQueryable<TEntity> RecuperarTodos(params Expression<Func<TEntity, dynamic>>[] expressoes)
        {
            var query = _bancoContexto.Set<TEntity>().AsQueryable();
            foreach (var item in expressoes)
                query = query.Include(item);
            return query;
        }

        public TEntity RecuperarPorId(object id, params Expression<Func<TEntity, dynamic>>[] expressoes)
        {
            var entity = _bancoContexto.Set<TEntity>();
            foreach (var item in expressoes)
                entity.Include(item);

            return entity.Find(id);
        }

        public void Inserir(TEntity entity)
        {
            _bancoContexto.Entry(entity).State = EntityState.Added;
            _bancoContexto.Set<TEntity>().Add(entity);
            _bancoContexto.SaveChanges();
        }

        public TEntity InserirERecuperar(TEntity entity)
        {
            _bancoContexto.Entry(entity).State = EntityState.Added;
            var resultado = _bancoContexto.Set<TEntity>().Add(entity);
            _bancoContexto.SaveChanges();
            return resultado.Entity;
        }

        public void Editar(TEntity entity)
        {
            _bancoContexto.Entry(entity).State = EntityState.Modified;
            _bancoContexto.Set<TEntity>().Update(entity);
            _bancoContexto.SaveChanges();
        }

        public void Excluir(TEntity entity)
        {
            _bancoContexto.Entry(entity).State = EntityState.Deleted;
            _bancoContexto.Set<TEntity>().Remove(entity);
            _bancoContexto.SaveChanges();
        }
    }
}
