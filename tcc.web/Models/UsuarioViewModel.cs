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
        public UsuarioViewModel()
        {
            ListaFuncionalidadeViewModel = new List<FuncionalidadeViewModel>();
        }

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

        public List<FuncionalidadeViewModel> ListaFuncionalidadeViewModel { get; set; }

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

    public class EditarUsuarioViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Favor informar um usuário")]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        public virtual string Senha { get; set; }

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
        public List<FuncionalidadeViewModel> ListaFuncionalidadeViewModel { get; set; }

        public static EditarUsuarioViewModel MapearViewModel(UsuarioRetorno model)
        {
            if (model == null) return new EditarUsuarioViewModel();

            var viewmodel = new EditarUsuarioViewModel()
            {
                UsuarioId = model.UsuarioId,
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
                UsuarioId = this.UsuarioId,
                Login = this.Login,
                Senha = string.IsNullOrEmpty(this.Senha) ? "" : this.Senha,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                Email = this.Email,
            };
        }
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

    public class FuncionalidadeViewModel
    {
        public int? FuncionalidadeId { get; set; }

        [Required(ErrorMessage = "Favor informar um código")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Favor informar a descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool PertenceUsuario { get; set; } = false;

        public static FuncionalidadeViewModel MapearViewModel(FuncionalidadeRetorno model)
        {
            if (model == null) return new FuncionalidadeViewModel();

            var viewmodel = new FuncionalidadeViewModel()
            {
                FuncionalidadeId = model.FuncionalidadeId,
                Codigo = model.Codigo,
                Descricao = model.Descricao,
            };

            return viewmodel;
        }

    }

}
