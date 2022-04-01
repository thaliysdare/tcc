using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
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
                                               .OrderByDescending(x => x.Ativo)
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
            #region[Validação]
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var servicoEnvio = viewModel.MapearModel();
                _servicoService.Inserir(servicoEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        [Route("editar")]
        public IActionResult Editar(ServicoViewModel viewModel)
        {
            #region[Validação]
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var servicoEnvio = viewModel.MapearModel();
                _servicoService.Editar(servicoEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        #region[Excluir]
        [HttpGet]
        [Route("excluir/{id}")]
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
