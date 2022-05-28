using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.webapi.Enums;

namespace tcc.webapi.Models.DTO
{
    public class OrcamentoEnvioDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int VeiculoId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime DataPrevisao { get; set; }

        [MaxLength(1000)]
        public string Observacao { get; set; }

        public StatusOrcamentoEnum Situacao { get; set; }

        [Required]
        public List<ServicoOrcamentoEnvioDTO> ListaItensServicos { get; set; }

        public Orcamento MapearModel()
        {
            return new Orcamento()
            {
                UsuarioId = this.UsuarioId,
                VeiculoId = this.VeiculoId,
                ClienteId = this.ClienteId,
                DataPrevisao = this.DataPrevisao,
                Observacao = this.Observacao,
                IdcStatusOrcamento = this.Situacao
            };
        }
    }

    public class OrcamentoRetornoDTO
    {
        public int OrcamentoId { get; set; }
        public int? OrdemServicoId { get; set; }
        public int UsuarioId { get; set; }
        public int VeiculoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Observacao { get; set; }
        public double ValorOrcamento { get; set; }
        public int Situacao { get; set; }
        public List<ServicoOrcamentoRetornoDTO> ListaItensServicos { get; set; }

        public ClienteRetornoDTO Cliente { get; set; }
        public VeiculoRetornoDTO Veiculo { get; set; }
        public UsuarioRetornoDTO Usuario { get; set; }

        public static OrcamentoRetornoDTO MapearDTO(Orcamento model)
        {
            var ordemServicoDTO = new OrcamentoRetornoDTO()
            {
                OrcamentoId = model.OrcamentoId,
                OrdemServicoId = model.OrdemServicoId,
                UsuarioId = model.UsuarioId,
                VeiculoId = model.VeiculoId,
                ClienteId = model.ClienteId,
                DataPrevisao = model.DataPrevisao,
                Observacao = model.Observacao,
                ValorOrcamento = model.ValorOrcamento,
                Situacao = (int)model.IdcStatusOrcamento
            };

            if (model.ServicoOrcamento != null && model.ServicoOrcamento.Any())
                ordemServicoDTO.ListaItensServicos = model.ServicoOrcamento.Select(x => ServicoOrcamentoRetornoDTO.MapearDTO(x)).ToList();

            ordemServicoDTO.Cliente = ClienteRetornoDTO.MapearDTO(model.Cliente);
            ordemServicoDTO.Veiculo = VeiculoRetornoDTO.MapearDTO(model.Veiculo);
            ordemServicoDTO.Usuario = UsuarioRetornoDTO.MapearDTO(model.Usuario);

            return ordemServicoDTO;
        }
    }

    public class OrcamentoEnvioPeriodoDTO
    {
        [Required]
        public DateTime DataInicial { get; set; }
        [Required]
        public DateTime DataFinal { get; set; }
    }
}
