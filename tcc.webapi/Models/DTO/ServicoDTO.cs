using System.ComponentModel.DataAnnotations;

namespace tcc.webapi.Models.DTO
{
    public class ServicoEnvioDTO
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        public string DescricaoResumida { get; set; }

        [Required]
        public double Valor { get; set; }

        public Servico MapearModel()
        {
            return new Servico()
            {
                Descricao = this.Descricao,
                DescricaoResumida = this.DescricaoResumida,
                Valor = this.Valor
            };
        }
    }

    public class ServicoRetornoDTO
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; }

        public static ServicoRetornoDTO MapearDTO(Servico model)
        {
            return new ServicoRetornoDTO()
            {
                ServicoId = model.ServicoId,
                Descricao = model.Descricao,
                DescricaoResumida = model.DescricaoResumida,
                Valor = model.Valor,
                Ativo = model.IdcStatusServico == Enums.StatusServicoEnum.Ativo
            };
        }
    }
}
