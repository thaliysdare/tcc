using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using tcc.web.Models.API;

namespace tcc.web.Models
{
    public class UsuariosViewModel
    {
        public List<UsuarioGridViewModel> ListaUsuarios { get; set; }

        public UsuariosViewModel()
        {
            ListaUsuarios = new List<UsuarioGridViewModel>();
        }
    }
    
    public class UsuarioViewModel
    {
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "Favor informar um usuário")]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Favor informar a senha")]
        [Display(Name = "Senha")]
        public virtual string Senha { get; set; }

        [Required(ErrorMessage = "Favor confirmar a senha")]
        [Display(Name = "Confirmação senha")]
        public virtual string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "Favor informar um nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor informar um sobrenome")]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Favor informar um email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Deseja alterar a senha?")]
        public bool AlterarSenha { get; set; } = false;

        public bool Ativo { get; set; }

        public static UsuarioViewModel MapearViewModel(UsuarioRetorno model)
        {
            if (model == null) return new UsuarioViewModel();

            var viewmodel = new UsuarioViewModel()
            {
                Login = model.Login,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                Ativo = model.Ativo,
            };

            return viewmodel;
        }

        public UsuarioEnvio MapearModel()
        {
            return new UsuarioEnvio()
            {
                Login = this.Login,
                Senha = this.Senha,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                Email = this.Email,
            };
        }
    }

    public class EditarUsuarioViewModel : UsuarioViewModel
    {
        [Display(Name = "Senha")]
        public override string Senha { get; set; }

        [Display(Name = "Confirmação senha")]
        public override string ConfirmaSenha { get; set; }
    }

    public class UsuarioGridViewModel
    {
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Situacao { get; set; }

        public static UsuarioGridViewModel MapearViewModel(UsuarioRetorno model)
        {
            if (model == null) return new UsuarioGridViewModel();

            return new UsuarioGridViewModel()
            {
                UsuarioId = model.UsuarioId,
                NomeCompleto = model.NomeCompleto,
                Email = model.Email,
                Situacao = model.Ativo ? "Ativo" : "Inativo"
            };
        }
    }

}
