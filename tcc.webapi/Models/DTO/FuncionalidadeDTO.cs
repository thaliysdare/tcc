using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.webapi.Enums;

namespace tcc.webapi.Models.DTO
{
    public class FuncionalidadeEnvioDTO
    {
        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public Funcionalidade MapearModel()
        {
            return new Funcionalidade()
            {
                Codigo = this.Codigo,
                Descricao = this.Descricao,
            };
        }
    }

    public class FuncionalidadeRetornoDTO
    {
        public int FuncionalidadeId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public static FuncionalidadeRetornoDTO MapearDTO(Funcionalidade model)
        {
            if (model == null) return null;
            return new FuncionalidadeRetornoDTO()
            {
                FuncionalidadeId = model.FuncionalidadeId,
                Codigo = model.Codigo,
                Descricao = model.Descricao,
            };
        }
    }
}
