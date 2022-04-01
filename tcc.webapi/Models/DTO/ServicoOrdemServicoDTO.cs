using System.ComponentModel.DataAnnotations;

namespace tcc.webapi.Models.DTO
{
    public class ServicoOrdemServicoEnvioDTO
    {
        public int? ServicoOrdemServicoId { get; set; }
        public int? OrdemServicoId { get; set; }
        [Required]
        public int ServicoId { get; set; }
        [Required]
        public double Valor { get; set; }

        public ServicoOrdemServico MapearModel()
        {
            var model = new ServicoOrdemServico()
            {
                ServicoId = this.ServicoId,
                Valor = this.Valor
            };

            if (this.ServicoOrdemServicoId.HasValue)
            {
                model.ServicoOrdemServicoId = this.ServicoOrdemServicoId.Value;
                model.OrdemServicoId = this.OrdemServicoId.Value;
            }
            return model;
        }
    }

    public class ServicoOrdemServicoRetornoDTO
    {
        public int ServicoOrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public int OrdemServicoId { get; set; }
        public double Valor { get; set; }

        public ServicoRetornoDTO Servico { get; set; }

        public static ServicoOrdemServicoRetornoDTO MapearDTO(ServicoOrdemServico model)
        {
            if (model == null) return null;

            var dto = new ServicoOrdemServicoRetornoDTO()
            {
                ServicoOrdemServicoId = model.ServicoOrdemServicoId,
                ServicoId = model.ServicoId,
                OrdemServicoId = model.OrdemServicoId,
                Valor = model.Valor
            };

            dto.Servico = ServicoRetornoDTO.MapearDTO(model.Servico);
            return dto;
        }
    }
}
