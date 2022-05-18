using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tcc.webapi.Models
{
    [Table("UsuarioFuncionalidade")]
    public class UsuarioFuncionalidade
    {
        public int UsuarioFuncionalidadeId { get; set; }

        [Required]
        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        [Required]
        [ForeignKey(nameof(Funcionalidade))]
        public int FuncionalidadeId { get; set; }

        public virtual Funcionalidade Funcionalidade { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
