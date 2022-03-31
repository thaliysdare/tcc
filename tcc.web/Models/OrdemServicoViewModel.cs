using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.web.Models.API;
using tcc.web.Utils;

namespace tcc.web.Models
{
    public class OrdemServicosViewModel
    {
        public List<OrdemServicoGridViewModel> ListaOS { get; set; }

        public OrdemServicosViewModel()
        {
            ListaOS = new List<OrdemServicoGridViewModel>();
        }
    }

    public class OrdemServicoViewModel
    {
        [Display(Name = "OS")]
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

        [Required(ErrorMessage = "Favor informar data de entrada do veículo")]
        [Display(Name = "Data entrada")]
        public DateTime DataEntrada { get; set; }

        [Required(ErrorMessage = "Favor informar data de previsão")]
        [Display(Name = "Data previsão")]
        public DateTime DataPrevisao { get; set; }

        [Display(Name = "Data saída")]
        public DateTime? DataSaida { get; set; }

        [Required(ErrorMessage = "Favor informar o km atual do veículo")]
        [Display(Name = "Km atual")]
        public int KMAtual { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Situação")]
        public int Situacao { get; set; }
        public List<SelectListItem> ListaSituacao { get; set; }

        public List<OrdemServicoItensViewModel> ListaItens { get; set; }
        public List<OrdemServicoServicosViewModel> ListaServicos { get; set; }

        [Required(ErrorMessage = "Favor informar pelo menos um serviço para a OS")]
        public List<int> ListaServicosEscolhidosId { get; set; }

        public OrdemServicoViewModel()
        {
            ListaVeiculos = new List<SelectListItem>();
            ListaClientes = new List<SelectListItem>();
            ListaItens = new List<OrdemServicoItensViewModel>();
            ListaServicos = new List<OrdemServicoServicosViewModel>();
            ListaServicosEscolhidosId = new List<int>();
        }

        public static OrdemServicoViewModel MapearViewModel(OrdemServicoRetorno model)
        {
            var viewModel = new OrdemServicoViewModel()
            {
                OrdemServicoId = model.OrdemServicoId,
                UsuarioId = model.UsuarioId,
                UsuarioNome = model.Usuario.NomeCompleto,
                VeiculoId = model.VeiculoId,
                VeiculoPlaca = model.Veiculo.Placa,
                ClienteId = model.ClienteId,
                ClienteNome = model.Cliente.NomeCompleto,
                DataEntrada = model.DataEntrada,
                DataPrevisao = model.DataPrevisao,
                DataSaida = model.DataSaida,
                KMAtual = model.KMAtual,
                Observacao = model.Observacao,
            };

            viewModel.ListaSituacao = EnumExtensions.GetEnumSelectList<OrdemServicoSituacaoEnum>()
                                                    .Where(x => x.Value != ((int)OrdemServicoSituacaoEnum.OSGerada).ToString())
                                                    .ToList();
            viewModel.ListaSituacao.ForEach(x => x.Selected = (x.Value == model.Situacao.ToString()));

            if (model.ListaItensServicos != null)
                viewModel.ListaItens = model.ListaItensServicos.Select(x => OrdemServicoItensViewModel.MapearViewModel(x)).ToList();

            return viewModel;
        }

        public OrdemServicoEnvio MapearModel()
        {
            return new OrdemServicoEnvio()
            {
                OrdemServicoId = this.OrdemServicoId,
                UsuarioId = this.UsuarioId,
                VeiculoId = this.VeiculoId,
                ClienteId = this.ClienteId,
                DataEntrada = this.DataEntrada,
                DataPrevisao = this.DataPrevisao,
                DataSaida = this.DataSaida,
                KMAtual = this.KMAtual,
                Observacao = this.Observacao,
                Situacao = this.Situacao,
                ListaItensServicos = this.ListaItens.Select(x => new ServicoOrdemServicoEnvio()
                {
                    ServicoOrdemServicoId = x.ServicoOrdemServicoId,
                    OrdemServicoId = x.OrdemServicoId,
                    ServicoId = x.ServicoId,
                    Valor = x.Valor
                }).ToList()
            };
        }
    }

    public class OrdemServicoGridViewModel
    {
        public int OrdemServicoId { get; set; }
        public string NomeCliente { get; set; }
        public string PlacaVeiculo { get; set; }
        public string Marca { get; set; }
        public string DataPrevisao { get; set; }
        public string ValorOS { get; set; }
        public string Situacao { get; set; }

        public static OrdemServicoGridViewModel MapearViewModel(OrdemServicoRetorno model)
        {
            var viewModel = new OrdemServicoGridViewModel()
            {
                OrdemServicoId = model.OrdemServicoId,
                NomeCliente = model.Cliente.NomeCompleto,
                PlacaVeiculo = model.Veiculo.Placa,
                Marca = model.Veiculo.Marca,
                DataPrevisao = model.DataPrevisao.ToString("dd/MM/yyyy"),
                ValorOS = "R$" + model.ValorOrdemServico.ToString(),
                Situacao = model.Situacao.GetDescription()
            };
            return viewModel;
        }
    }

    public class OrdemServicoItensViewModel
    {
        public int? ServicoOrdemServicoId { get; set; }
        public int? OrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }

        public static OrdemServicoItensViewModel MapearViewModel(ServicoOrdemServicoRetorno model)
        {
            if (model == null) return new OrdemServicoItensViewModel();

            var viewmodel = new OrdemServicoItensViewModel()
            {
                ServicoOrdemServicoId = model.ServicoOrdemServicoId,
                OrdemServicoId = model.OrdemServicoId,
                ServicoId = model.ServicoId,
                Valor = model.Valor
            };

            if (model.Servico != null)
                viewmodel.DescricaoResumida = model.Servico.DescricaoResumida;

            return viewmodel;
        }
    }

    public class OrdemServicoServicosViewModel
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }

        public static OrdemServicoServicosViewModel MapearViewModel(ServicoRetorno servico)
        {
            if (servico == null) return new OrdemServicoServicosViewModel();

            var model = new OrdemServicoServicosViewModel()
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
