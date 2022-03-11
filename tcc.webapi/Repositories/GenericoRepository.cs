using Microsoft.EntityFrameworkCore;
using System.Linq;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class GenericoRepository<TEntity> : IGenericoRepository<TEntity>
        where TEntity : class
    {
        private readonly BancoContexto _bancoContexto;

        public GenericoRepository(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
        }

        public IQueryable<TEntity> RecuperarTodos()
        {
            return _bancoContexto.Set<TEntity>()
                                 .AsNoTracking();
        }

        public void Inserir(TEntity entity)
        {
            _bancoContexto.Set<TEntity>().Add(entity);
        }

        public void Editar(TEntity entity)
        {
            _bancoContexto.Set<TEntity>().Update(entity);
        }

        public void Excluir(TEntity entity)
        {
            _bancoContexto.Set<TEntity>().Remove(entity);
        }
    }
}
