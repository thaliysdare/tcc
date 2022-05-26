using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IServicoOrdemServicoRepository _servicoOrdemServicoRepository;

        public OrdemServicoService(IOrdemServicoRepository ordemServicoRepository,
                                   IServicoRepository servicoRepository,
                                   IServicoOrdemServicoRepository servicoOrdemServicoRepository)
        {
            _ordemServicoRepository = ordemServicoRepository;
            _servicoRepository = servicoRepository;
            _servicoOrdemServicoRepository = servicoOrdemServicoRepository;
        }

        public OrdemServico Inserir(OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens)
        {
            var modelNovo = default(OrdemServico);
            using (var scope = new TransactionScope())
            {
                if (listaItens == null || !listaItens.Any()) throw new Exception("Não é possível cadastrar ordem de serviço sem item");

                model.IdcStatusOrdemServico = Enums.StatusOrdemServicoEnum.OSGerada;
                model.ValorOrdemServico = listaItens.Sum(x => x.Valor);
                modelNovo = _ordemServicoRepository.InserirERecuperar(model);

                foreach (var item in listaItens)
                {
                    if (!_servicoRepository.VerificarServicoAtivo(item.ServicoId))
                        throw new Exception($"Existe serviço não está liberado para uso");

                    var servicoOrdemServico = item.MapearModel();
                    servicoOrdemServico.OrdemServicoId = modelNovo.OrdemServicoId;
                    _servicoOrdemServicoRepository.Inserir(servicoOrdemServico);
                }
                scope.Complete();
            }
            return modelNovo;
        }

        public void Editar(int id, OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens)
        {
            var originalModel = _ordemServicoRepository.RecuperarPorId(id);
            VerificarLiberadoParaAlteracao(originalModel);

            using (var scope = new TransactionScope())
            {
                originalModel.DataPrevisao = model.DataPrevisao;
                originalModel.DataSaida = model.DataSaida;
                originalModel.KMAtual = model.KMAtual;
                originalModel.Observacao = model.Observacao;

                originalModel.IdcStatusOrdemServico = model.IdcStatusOrdemServico;
                if (originalModel.IdcStatusOrdemServico == Enums.StatusOrdemServicoEnum.OSFinalizada)
                    originalModel.DataSaida = DateTime.Now;

                var listaAIncluir = listaItens.Where(x => !x.ServicoOrdemServicoId.HasValue).ToList();
                var listaAVerificar = listaItens.Where(x => x.ServicoOrdemServicoId.HasValue).ToList();
                var listaIdsAVerificar = listaAVerificar.Select(x => x.ServicoOrdemServicoId).ToList();
                var listaServicosExcluidos = originalModel.ServicoOrdemServico.Where(x => !listaIdsAVerificar.Contains(x.ServicoOrdemServicoId)).ToList();

                if (listaAIncluir.Count == 0
                    && listaServicosExcluidos.Count == originalModel.ServicoOrdemServico.Count)
                    throw new Exception("Não é permitido ordem de serviço sem itens");

                foreach (var item in listaAIncluir)
                {
                    var servicoOrdemServico = item.MapearModel();
                    if (!_servicoRepository.VerificarServicoAtivo(item.ServicoId))
                        throw new Exception($"Existe serviço não está liberado para uso");

                    servicoOrdemServico.OrdemServicoId = originalModel.OrdemServicoId;
                    _servicoOrdemServicoRepository.Inserir(servicoOrdemServico);
                }

                foreach (var item in listaServicosExcluidos)
                {
                    _servicoOrdemServicoRepository.Excluir(item);
                }

                foreach (var item in listaAVerificar)
                {
                    var originalServicoOrdemServico = originalModel.ServicoOrdemServico.FirstOrDefault(x => x.ServicoOrdemServicoId == item.ServicoOrdemServicoId.Value);
                    originalServicoOrdemServico.Valor = item.Valor;
                    _servicoOrdemServicoRepository.Editar(originalServicoOrdemServico);
                }

                originalModel.ValorOrdemServico = originalModel.ServicoOrdemServico.Sum(x => x.Valor);

                _ordemServicoRepository.Editar(originalModel);
                scope.Complete();
            }
        }

        #region[Metodos auxiliares]
        private void VerificarLiberadoParaAlteracao(OrdemServico model)
        {
            if (model == null)
                throw new System.Exception("OrdemServico não encontrado");
            if (model.IdcStatusOrdemServico == Enums.StatusOrdemServicoEnum.OSCancelada
                || model.IdcStatusOrdemServico == Enums.StatusOrdemServicoEnum.OSFinalizada)
                throw new System.Exception("OrdemServico com status que não permite mais alterações");
        }
        #endregion
    }
}
