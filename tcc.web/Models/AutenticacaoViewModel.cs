using System.ComponentModel.DataAnnotations;
using tcc.web.Models.API;

namespace tcc.web.Models
{
    public class AutenticacaoViewModel
    {
        [Required(ErrorMessage = "Favor informar um usuário")]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Favor informar a senha")]
        [Display(Name = "Senha")]
        public virtual string Senha { get; set; }

        public UsuarioAutenticacao MapearModel()
        {
            return new UsuarioAutenticacao()
            {
                Login = this.Login,
                Senha = this.Senha,
            };
        }
    }

}
