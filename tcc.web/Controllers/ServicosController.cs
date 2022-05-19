using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "NV2")]
    public class ServicosController : GenericoController
    {
        private readonly IServicoService _servicoService;
        public ServicosController(IServicoService servicoService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _servicoService = servicoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ServicosViewModel()
            {
                ListaServicos = _servicoService.RecuperarTodos()
                                               .OrderByDescending(x => x.ServicoId)
                                               .Select(x => ServicoGridViewModel.MapearViewModel(x))
                                               .ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        [Route("cadastrar")]
        public IActionResult CarregarCadastrar()
        {
            var viewModel = new ServicoViewModel();
            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar(ServicoViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);


            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var servicoEnvio = viewModel.MapearModel();
                _servicoService.Inserir(servicoEnvio);

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
            var servico = _servicoService.Recuperar(id);
            var viewModel = ServicoViewModel.MapearViewModel(servico);

            if (!servico.Ativo)
                return View("Inativo", viewModel);
            return View("Editar", viewModel);
        }

        [HttpPut]
        [Route("editar")]
        public IActionResult Editar(ServicoViewModel viewModel)
        {
            #region[Validação]
            #endregion

            if (!ModelState.IsValid) return View(viewModel);


            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                _servicoService.Editar(model.ServicoId.Value, model);

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
        [Authorize(Roles = "NV3")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _servicoService.Excluir(id);
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
