﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tcc.webapi.Enums;

namespace tcc.webapi.Models.DTO
{
    public class UsuarioEnvioDTO
    {
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

        public Usuario MapearModel()
        {
            return new Usuario()
            {
                Login = this.Login,
                Senha = this.Senha,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                Email = this.Email,
            };
        }
    }

    public class UsuarioRetornoDTO
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

        public static UsuarioRetornoDTO MapearDTO(Usuario model)
        {
            return new UsuarioRetornoDTO()
            {
                UsuarioId = model.UsuarioId,
                Login = model.Login,
                Token = model.Senha,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                Ativo = model.IdcStatusUsuario == StatusUsuarioEnum.Ativo,
            };
        }
    }
}
