using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.web.Models.API;
using tcc.web.Utils;

namespace tcc.web.Models
{
    public class OrcamentosViewModel
    {
        public List<OrcamentoGridViewModel> ListaOrcamento { get; set; }

        public OrcamentosViewModel()
        {
            ListaOrcamento = new List<OrcamentoGridViewModel>();
        }
    }

    public class OrcamentoViewModel
    {
        [Display(Name = "Orçamento")]
        public int? OrcamentoId { get; set; }

        [Display(Name = "Ordem serviço")]
        public int? OrdemServicoId { get; set; }

        [Required(ErrorMessage = "Favor informar um usuário")]
        public int UsuarioId { get; set; }

        [Display(Name = "Usuário")]
        public string UsuarioNome { get; set; }

        [Required(ErrorMessage = "Favor informar um veículo")]
        [Display(Name = "Veículo")]
        public int VeiculoId { get; set; }

        [Display(Name = "Placa")]
        public string VeiculoPlaca { get; set; }

        public List<SelectListItem> ListaVeiculos { get; set; }

        [Required(ErrorMessage = "Favor informar um cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Display(Name = "Cliente")]
        public string ClienteNome { get; set; }

        public List<SelectListItem> ListaClientes { get; set; }

        [Required(ErrorMessage = "Favor informar data de previsão")]
        [Display(Name = "Data previsão")]
        public DateTime DataPrevisao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Situação")]
        public int Situacao { get; set; }
        public string SituacaoDescricao { get; set; }
        public List<SelectListItem> ListaSituacao { get; set; }

        public List<OrcamentoItensViewModel> ListaItens { get; set; }
        public List<OrcamentoServicosViewModel> ListaServicos { get; set; }

        [Required(ErrorMessage = "Favor informar pelo menos um serviço para o orçamento")]
        public List<int> ListaServicosEscolhidosId { get; set; }

        public OrcamentoViewModel()
        {
            ListaVeiculos = new List<SelectListItem>();
            ListaClientes = new List<SelectListItem>();
            ListaItens = new List<OrcamentoItensViewModel>();
            ListaServicos = new List<OrcamentoServicosViewModel>();
            ListaServicosEscolhidosId = new List<int>();
        }

        public static OrcamentoViewModel MapearViewModel(OrcamentoRetorno model)
        {
            var viewModel = new OrcamentoViewModel()
            {
                OrcamentoId = model.OrcamentoId,
                OrdemServicoId = model.OrdemServicoId,
                UsuarioId = model.UsuarioId,
                UsuarioNome = model.Usuario.NomeCompleto,
                VeiculoId = model.VeiculoId,
                VeiculoPlaca = model.Veiculo.Placa,
                ClienteId = model.ClienteId,
                ClienteNome = model.Cliente.NomeCompleto,
                DataPrevisao = model.DataPrevisao,
                Observacao = model.Observacao,
                Situacao = (int)model.Situacao,
                SituacaoDescricao = model.Situacao.GetDescription()
            };

            viewModel.ListaSituacao = EnumExtensions.GetEnumSelectList<OrcamentoSituacaoEnum>()
                                                    .ToList();

            viewModel.ListaSituacao.ForEach(x => x.Selected = (x.Value == model.Situacao.ToString()));

            if (model.ListaItensServicos != null)
                viewModel.ListaItens = model.ListaItensServicos.Select(x => OrcamentoItensViewModel.MapearViewModel(x)).ToList();

            return viewModel;
        }

        public OrcamentoEnvio MapearModel()
        {
            return new OrcamentoEnvio()
            {
                OrcamentoId = this.OrcamentoId,
                UsuarioId = this.UsuarioId,
                VeiculoId = this.VeiculoId,
                ClienteId = this.ClienteId,
                DataPrevisao = this.DataPrevisao,
                Observacao = this.Observacao,
                Situacao = this.Situacao,
                ListaItensServicos = this.ListaItens.Select(x => new ServicoOrcamentoEnvio()
                {
                    ServicoOrcamentoId = x.ServicoOrcamentoId,
                    OrcamentoId = x.OrcamentoId,
                    ServicoId = x.ServicoId,
                    Valor = x.Valor
                }).ToList()
            };
        }
    }

    public class OrcamentoGridViewModel
    {
        public int OrcamentoId { get; set; }
        public string NomeCliente { get; set; }
        public string PlacaVeiculo { get; set; }
        public string Marca { get; set; }
        public string DataPrevisao { get; set; }
        public string ValorOrcamento { get; set; }
        public string SituacaoDescricao { get; set; }
        public int? OrdemServicoId { get; set; }
        public int Situacao { get; set; }

        public static OrcamentoGridViewModel MapearViewModel(OrcamentoRetorno model)
        {
            var viewModel = new OrcamentoGridViewModel()
            {
                OrcamentoId = model.OrcamentoId,
                NomeCliente = model.Cliente.NomeCompleto,
                PlacaVeiculo = model.Veiculo.Placa,
                Marca = model.Veiculo.Marca,
                DataPrevisao = model.DataPrevisao.ToString("dd/MM/yyyy"),
                ValorOrcamento = "R$" + model.ValorOrcamento.ToString(),
                SituacaoDescricao = model.Situacao.GetDescription(),
                Situacao = (int)model.Situacao,
                OrdemServicoId = model.OrdemServicoId
            };
            return viewModel;
        }
    }

    public class OrcamentoItensViewModel
    {
        public int? ServicoOrcamentoId { get; set; }
        public int? OrcamentoId { get; set; }
        public int ServicoId { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }

        public static OrcamentoItensViewModel MapearViewModel(ServicoOrcamentoRetorno model)
        {
            if (model == null) return new OrcamentoItensViewModel();

            var viewmodel = new OrcamentoItensViewModel()
            {
                ServicoOrcamentoId = model.ServicoOrcamentoId,
                OrcamentoId = model.OrcamentoId,
                ServicoId = model.ServicoId,
                Valor = model.Valor
            };

            if (model.Servico != null)
                viewmodel.DescricaoResumida = model.Servico.DescricaoResumida;

            return viewmodel;
        }
    }

    public class OrcamentoServicosViewModel
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }

        public static OrcamentoServicosViewModel MapearViewModel(ServicoRetorno servico)
        {
            if (servico == null) return new OrcamentoServicosViewModel();

            var model = new OrcamentoServicosViewModel()
            {
                ServicoId = servico.ServicoId,
                Descricao = servico.Descricao,
                DescricaoResumida = servico.DescricaoResumida,
                Valor = servico.Valor,
            };

            return model;
        }
    }

}
