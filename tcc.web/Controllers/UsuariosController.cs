using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;
using tcc.web.Utils;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "NV3")]
    public class UsuariosController : GenericoController
    {
        private readonly IFuncionalidadeService _funcionalidadeService;
        public UsuariosController(IUsuarioService usuarioService, IFuncionalidadeService funcionalidadeService) : base(usuarioService)
        {
            _funcionalidadeService = funcionalidadeService;
        }

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
            viewModel.ListaFuncionalidadeViewModel = _funcionalidadeService.RecuperarTodos().OrderBy(x => x.FuncionalidadeId).Select(x => FuncionalidadeViewModel.MapearViewModel(x)).ToList();

            var i = 1;
            viewModel.ListaFuncionalidadeViewModel.ForEach(x =>
            {
                x.Nivel = i;
                i++;
            });
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
            viewModel.ListaFuncionalidadeViewModel = _funcionalidadeService.RecuperarTodos().OrderBy(x => x.FuncionalidadeId).Select(x => FuncionalidadeViewModel.MapearViewModel(x)).ToList();

            if (model.ListaFuncionalidade != null && model.ListaFuncionalidade.Count > 0)
                viewModel.ListaFuncionalidadeViewModel.ForEach(x => x.PertenceUsuario = model.ListaFuncionalidade.Contains(x.Codigo));

            var i = 1;
            viewModel.ListaFuncionalidadeViewModel.ForEach(x =>
            {
                x.Nivel = i;
                i++;
            });

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
