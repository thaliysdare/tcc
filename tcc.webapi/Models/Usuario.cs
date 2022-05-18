using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public StatusUsuarioEnum IdcStatusUsuario { get; set; }

        public virtual ICollection<Funcionalidade> Funcionalidades { get; set; }
    }
}
