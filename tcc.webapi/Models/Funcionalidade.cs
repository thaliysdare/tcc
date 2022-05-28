using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("Funcionalidade")]
    public class Funcionalidade
    {
        public int FuncionalidadeId { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public int Nivel { get; set; }
    }
}
