using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using tcc.webapi.Enums;

namespace tcc.webapi.Models.DTO
{
    public class UsuarioEnvioDTO
    {
        [Required]
        public string Login { get; set; }

        public string Senha { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public string Email { get; set; }

        public List<int> ListaFuncionalidadeIds { get; set; }

        public Usuario MapearModel()
        {
            var usuario = new Usuario()
            {
                Login = this.Login,
                Senha = this.Senha,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                Email = this.Email,
            };

            if (ListaFuncionalidadeIds != null && ListaFuncionalidadeIds.Count > 0)
                usuario.UsuarioFuncionalidade = this.ListaFuncionalidadeIds.Select(x => new UsuarioFuncionalidade() { FuncionalidadeId = x }).ToList();

            return usuario;
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
        public List<string> ListaFuncionalidade { get; set; }

        public static UsuarioRetornoDTO MapearDTO(Usuario model)
        {
            if (model == null) return null;
            var retorno = new UsuarioRetornoDTO()
            {
                UsuarioId = model.UsuarioId,
                Login = model.Login,
                Token = model.Senha,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                Ativo = model.IdcStatusUsuario == StatusUsuarioEnum.Ativo,
            };

            if (model.UsuarioFuncionalidade != null && model.UsuarioFuncionalidade.All(x => x.Funcionalidade != null))
                retorno.ListaFuncionalidade = model.UsuarioFuncionalidade.Select(x => x.Funcionalidade.Codigo).ToList();

            return retorno;

        }
    }

    public class UsuarioAutenticacaoDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        public Usuario MapearModel()
        {
            return new Usuario()
            {
                Login = this.Login,
                Senha = this.Senha
            };
        }
    }
}
