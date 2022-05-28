using System.ComponentModel.DataAnnotations;

namespace tcc.webapi.Models.DTO
{
    public class ServicoOrcamentoEnvioDTO
    {
        public int? ServicoOrcamentoId { get; set; }
        public int? OrcamentoId { get; set; }
        [Required]
        public int ServicoId { get; set; }
        [Required]
        public double Valor { get; set; }

        public ServicoOrcamento MapearModel()
        {
            var model = new ServicoOrcamento()
            {
                ServicoId = this.ServicoId,
                Valor = this.Valor
            };

            if (this.ServicoOrcamentoId.HasValue)
            {
                model.ServicoOrcamentoId = this.ServicoOrcamentoId.Value;
                model.OrcamentoId = this.OrcamentoId.Value;
            }
            return model;
        }
    }

    public class ServicoOrcamentoRetornoDTO
    {
        public int ServicoOrcamentoId { get; set; }
        public int ServicoId { get; set; }
        public int OrcamentoId { get; set; }
        public double Valor { get; set; }

        public ServicoRetornoDTO Servico { get; set; }

        public static ServicoOrcamentoRetornoDTO MapearDTO(ServicoOrcamento model)
        {
            if (model == null) return null;

            var dto = new ServicoOrcamentoRetornoDTO()
            {
                ServicoOrcamentoId = model.ServicoOrcamentoId,
                ServicoId = model.ServicoId,
                OrcamentoId = model.OrcamentoId,
                Valor = model.Valor
            };

            dto.Servico = ServicoRetornoDTO.MapearDTO(model.Servico);
            return dto;
        }
    }
}
