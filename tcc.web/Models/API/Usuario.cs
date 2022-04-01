using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tcc.web.Models.API
{
    public class UsuarioEnvio
    {
        public int? UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }

    }

    public class UsuarioRetornoRoot
    {
        public List<UsuarioRetorno> Usuarios { get; set; }

    }

    public class UsuarioRetorno
    {
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public List<string> ListaPermissoes { get; set; }

        [JsonIgnore]
        public string NomeCompleto { get => $"{Nome} {Sobrenome}"; }
    }

    public class UsuarioAutenticacao
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
