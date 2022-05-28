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
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IServicoOrcamentoRepository _servicoOrcamentoRepository;
        private readonly IOrdemServicoService _ordemServicoService;

        public OrcamentoService(IOrcamentoRepository orcamentoRepository,
                                IServicoRepository servicoRepository,
                                IServicoOrcamentoRepository servicoOrcamentoRepository,
                                IOrdemServicoService ordemServicoService)
        {
            _orcamentoRepository = orcamentoRepository;
            _servicoRepository = servicoRepository;
            _servicoOrcamentoRepository = servicoOrcamentoRepository;
            _ordemServicoService = ordemServicoService;
        }

        public Orcamento Inserir(Orcamento model, List<ServicoOrcamentoEnvioDTO> listaItens)
        {
            var modelNovo = default(Orcamento);
            using (var scope = new TransactionScope())
            {
                if (listaItens == null || !listaItens.Any()) throw new Exception("Não é possível cadastrar orçamento sem item");

                model.DataOrcamento = DateTime.Now;
                model.IdcStatusOrcamento = Enums.StatusOrcamentoEnum.OrcamentoGerado;
                model.ValorOrcamento = listaItens.Sum(x => x.Valor);
                modelNovo = _orcamentoRepository.InserirERecuperar(model);

                foreach (var item in listaItens)
                {
                    if (!_servicoRepository.VerificarServicoAtivo(item.ServicoId))
                        throw new Exception($"Existe serviço não está liberado para uso");

                    var servicoOrcamento = item.MapearModel();
                    servicoOrcamento.OrcamentoId = modelNovo.OrcamentoId;
                    _servicoOrcamentoRepository.Inserir(servicoOrcamento);
                }
                scope.Complete();
            }
            return modelNovo;
        }

        public void Editar(int id, Orcamento model, List<ServicoOrcamentoEnvioDTO> listaItens)
        {
            var originalModel = _orcamentoRepository.RecuperarPorId(id);
            VerificarLiberadoParaAlteracao(originalModel);

            using (var scope = new TransactionScope())
            {
                originalModel.DataPrevisao = model.DataPrevisao;
                originalModel.Observacao = model.Observacao;

                originalModel.IdcStatusOrcamento = model.IdcStatusOrcamento;

                var listaAIncluir = listaItens.Where(x => !x.ServicoOrcamentoId.HasValue).ToList();
                var listaAVerificar = listaItens.Where(x => x.ServicoOrcamentoId.HasValue).ToList();
                var listaIdsAVerificar = listaAVerificar.Select(x => x.ServicoOrcamentoId).ToList();
                var listaServicosExcluidos = originalModel.ServicoOrcamento.Where(x => !listaIdsAVerificar.Contains(x.ServicoOrcamentoId)).ToList();

                if (listaAIncluir.Count == 0
                    && listaServicosExcluidos.Count == originalModel.ServicoOrcamento.Count)
                    throw new Exception("Não é permitido orçamento sem itens");

                foreach (var item in listaAIncluir)
                {
                    var servicoOrcamento = item.MapearModel();
                    if (!_servicoRepository.VerificarServicoAtivo(item.ServicoId))
                        throw new Exception($"Existe serviço não está liberado para uso");

                    servicoOrcamento.OrcamentoId = originalModel.OrcamentoId;
                    _servicoOrcamentoRepository.Inserir(servicoOrcamento);
                }

                foreach (var item in listaServicosExcluidos)
                {
                    _servicoOrcamentoRepository.Excluir(item);
                }

                foreach (var item in listaAVerificar)
                {
                    var originalServicoOrcamento = originalModel.ServicoOrcamento.FirstOrDefault(x => x.ServicoOrcamentoId == item.ServicoOrcamentoId.Value);
                    originalServicoOrcamento.Valor = item.Valor;
                    _servicoOrcamentoRepository.Editar(originalServicoOrcamento);
                }

                originalModel.ValorOrcamento = originalModel.ServicoOrcamento.Sum(x => x.Valor);
                _orcamentoRepository.Editar(originalModel);

                if (originalModel.IdcStatusOrcamento == Enums.StatusOrcamentoEnum.OSGerada)
                {
                    var ordemServico = new OrdemServico()
                    {
                        UsuarioId = originalModel.UsuarioId,
                        VeiculoId = originalModel.VeiculoId,
                        ClienteId = originalModel.ClienteId,
                        DataEntrada = DateTime.Now,
                        DataPrevisao = originalModel.DataPrevisao,
                        KMAtual = 0
                    };

                    var listaServico = new List<ServicoOrdemServicoEnvioDTO>();
                    foreach (var item in originalModel.ServicoOrcamento)
                    {
                        listaServico.Add(new ServicoOrdemServicoEnvioDTO
                        {
                            ServicoId = item.ServicoId,
                            Valor = item.Valor,
                        });
                    }

                    ordemServico = _ordemServicoService.Inserir(ordemServico, listaServico);
                    originalModel.OrdemServicoId = ordemServico.OrdemServicoId;
                }

                _orcamentoRepository.Editar(originalModel);
                scope.Complete();
            }
        }

        #region[Metodos auxiliares]
        private void VerificarLiberadoParaAlteracao(Orcamento model)
        {
            if (model == null)
                throw new System.Exception("Orcamento não encontrado");
            if (model.IdcStatusOrcamento == Enums.StatusOrcamentoEnum.OSGerada)
                throw new System.Exception("Orcamento com status que não permite mais alterações");
        }
        #endregion
    }
}
