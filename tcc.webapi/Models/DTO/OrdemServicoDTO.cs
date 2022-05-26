using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.webapi.Enums;

namespace tcc.webapi.Models.DTO
{
    public class OrdemServicoEnvioDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime DataEntrada { get; set; }

        [Required]
        public DateTime DataPrevisao { get; set; }

        public DateTime? DataSaida { get; set; }

        [Required]
        public int KMAtual { get; set; }

        [MaxLength(1000)]
        public string Observacao { get; set; }

        public StatusOrdemServicoEnum Situacao { get; set; }

        [Required]
        public List<ServicoOrdemServicoEnvioDTO> ListaItensServicos { get; set; }

        public OrdemServico MapearModel()
        {
            return new OrdemServico()
            {
                UsuarioId = this.UsuarioId,
                VeiculoId = this.VeiculoId,
                ClienteId = this.ClienteId,
                DataEntrada = this.DataEntrada,
                DataPrevisao = this.DataPrevisao,
                DataSaida = this.DataSaida,
                KMAtual = this.KMAtual,
                Observacao = this.Observacao,
                IdcStatusOrdemServico = this.Situacao
            };
        }
    }

    public class OrdemServicoRetornoDTO
    {
        public int OrdemServicoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataPrevisao { get; set; }
        public DateTime? DataSaida { get; set; }
        public int KMAtual { get; set; }
        public string Observacao { get; set; }
        public double ValorOrdemServico { get; set; }
        public int Situacao { get; set; }
        public List<ServicoOrdemServicoRetornoDTO> ListaItensServicos { get; set; }

        public ClienteRetornoDTO Cliente { get; set; }
        public VeiculoRetornoDTO Veiculo { get; set; }
        public UsuarioRetornoDTO Usuario { get; set; }

        public static OrdemServicoRetornoDTO MapearDTO(OrdemServico model)
        {
            var ordemServicoDTO = new OrdemServicoRetornoDTO()
            {
                OrdemServicoId = model.OrdemServicoId,
                UsuarioId = model.UsuarioId,
                VeiculoId = model.VeiculoId,
                ClienteId = model.ClienteId,
                DataEntrada = model.DataEntrada,
                DataPrevisao = model.DataPrevisao,
                DataSaida = model.DataSaida,
                KMAtual = model.KMAtual,
                Observacao = model.Observacao,
                ValorOrdemServico = model.ValorOrdemServico,
                Situacao = (int)model.IdcStatusOrdemServico
            };

            if (model.ServicoOrdemServico != null && model.ServicoOrdemServico.Any())
                ordemServicoDTO.ListaItensServicos = model.ServicoOrdemServico.Select(x => ServicoOrdemServicoRetornoDTO.MapearDTO(x)).ToList();

            ordemServicoDTO.Cliente = ClienteRetornoDTO.MapearDTO(model.Cliente);
            ordemServicoDTO.Veiculo = VeiculoRetornoDTO.MapearDTO(model.Veiculo);
            ordemServicoDTO.Usuario = UsuarioRetornoDTO.MapearDTO(model.Usuario);

            return ordemServicoDTO;
        }
    }

    public class OrdemServicoEnvioPeriodoDTO
    {
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
    }
}
