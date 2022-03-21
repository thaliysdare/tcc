﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPFOuCNPJ { get; set; }

        [Required]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        [ForeignKey(nameof(Endereco))]
        public int? EnderecoId { get; set; }

        [Required]
        public StatusClienteEnum IdcStatusCliente { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<Veiculo> Veiculo { get; set; }
    }
}
