using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class FuncionalidadeService : IFuncionalidadeService
    {
        private readonly IFuncionalidadeRepository _funcionalidadeRepository;

        public FuncionalidadeService(IFuncionalidadeRepository funcionalidadeRepository)
        {
            _funcionalidadeRepository = funcionalidadeRepository;
        }

        public Funcionalidade Inserir(Funcionalidade model)
        {
            var modelNovo = default(Funcionalidade);
            using (var scope = new TransactionScope())
            {
                modelNovo = _funcionalidadeRepository.InserirERecuperar(model);
                scope.Complete();
            }
            return modelNovo;
        }

        public void Editar(int id, Funcionalidade model)
        {
            var originalModel = _funcionalidadeRepository.RecuperarPorId(id);

            using (var scope = new TransactionScope())
            {
                originalModel.Descricao = model.Descricao;
                _funcionalidadeRepository.Editar(originalModel);
                scope.Complete();
            }
        }
    }
}
