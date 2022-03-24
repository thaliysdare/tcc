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
            return new ServicoOrdemServico()
            {
                ServicoId = this.ServicoId,
                Valor = this.Valor
            };
        }
    }

    public class ServicoOrdemServicoRetornoDTO
    {
        public int ServicoOrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public int OrdemServicoId { get; set; }
        public double Valor { get; set; }

        public static ServicoOrdemServicoRetornoDTO MapearDTO(ServicoOrdemServico model)
        {
            return new ServicoOrdemServicoRetornoDTO()
            {
                ServicoOrdemServicoId = model.ServicoOrdemServicoId,
                ServicoId = model.ServicoId,
                OrdemServicoId = model.OrdemServicoId,
                Valor = model.Valor
            };
        }
    }
}
