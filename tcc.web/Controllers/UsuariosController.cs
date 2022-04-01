using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : GenericoController
    {
        public UsuariosController(IUsuarioService usuarioService) : base(usuarioService) { }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new UsuariosViewModel()
            {
                ListaUsuarios = _usuarioService.RecuperarTodos()
                                               .Where(x => x.UsuarioId != RecuperarUsuarioLogado().UsuarioId)
                                               .OrderByDescending(x => x.UsuarioId)
                                               .Select(x => UsuarioGridViewModel.MapearViewModel(x))
                                               .ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        [Route("cadastrar")]
        public IActionResult CarregarCadastrar()
        {
            var viewModel = new UsuarioViewModel();
            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public JsonResult Cadastrar(UsuarioViewModel viewModel)
        {
            if (!viewModel.Senha.Equals(viewModel.ConfirmaSenha))
                ModelState.AddModelError("ErroServidor", "Senhas informadas não correspondem");

            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                _usuarioService.Inserir(model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult CarregarEditar(int id)
        {
            var model = _usuarioService.Recuperar(id);
            var viewModel = EditarUsuarioViewModel.MapearViewModel(model);

            if (!model.Ativo)
                return View("Inativo", viewModel);
            return View("Editar", viewModel);
        }

        [HttpPut]
        [Route("editar")]
        public JsonResult Editar(EditarUsuarioViewModel viewModel)
        {
            if (viewModel.AlterarSenha)
            {
                if (string.IsNullOrEmpty(viewModel.Senha))
                    ModelState.AddModelError("ErroServidor", "Favor informar a senha");
                else if (string.IsNullOrEmpty(viewModel.ConfirmaSenha))
                    ModelState.AddModelError("ErroServidor", "Favor informar a confirmação da senha");
                else if (!viewModel.Senha.Equals(viewModel.ConfirmaSenha))
                    ModelState.AddModelError("ErroServidor", "Senhas informadas não correspondem");
            }

            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                _usuarioService.Editar(viewModel.UsuarioId, model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.PUT));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        #region[Excluir]
        [HttpGet]
        [Route("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _usuarioService.Excluir(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
