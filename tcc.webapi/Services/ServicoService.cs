using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public Servico Inserir(Servico model)
        {
            var modelNovo = default(Servico);
            using (var scope = new TransactionScope())
            {
                model.IdcStatusServico = Enums.StatusServicoEnum.Ativo;
                modelNovo = _servicoRepository.InserirERecuperar(model);
                scope.Complete();
            }
            return modelNovo;
        }

        public void Editar(int id, Servico model)
        {
            var originalModel = _servicoRepository.RecuperarPorId(id);
            VerificarLiberadoParaAlteracao(originalModel);

            using (var scope = new TransactionScope())
            {
                originalModel.Descricao = model.Descricao;
                originalModel.DescricaoResumida = model.DescricaoResumida;
                originalModel.Valor = model.Valor;

                _servicoRepository.Editar(originalModel);
                scope.Complete();
            }
        }

        public void Inativar(int id)
        {
            using (var scope = new TransactionScope())
            {
                var originalModel = _servicoRepository.RecuperarPorId(id);
                originalModel.IdcStatusServico = Enums.StatusServicoEnum.Inativo;
                _servicoRepository.Editar(originalModel);

                scope.Complete();
            }
        }

        #region[Metodos auxiliares]
        private void VerificarLiberadoParaAlteracao(Servico model)
        {
            if (model == null)
                throw new System.Exception("Servico não encontrado");
            if (model.IdcStatusServico == Enums.StatusServicoEnum.Inativo)
                throw new System.Exception("Servico inativo não pode ser alterado");
        }
        #endregion
    }
}
