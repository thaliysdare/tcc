using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    public class VeiculosController : GenericoController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IClienteService _clienteService;
        public VeiculosController(IVeiculoService veiculoService, IClienteService clienteService)
        {
            _veiculoService = veiculoService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new VeiculoViewModel();
            return View(viewModel);
        }

        [HttpGet]
        [Route("cadastrar/{id}")]
        public IActionResult CarregarCadastrar(int id)
        {
            var cliente = _clienteService.Recuperar(id);
            var viewModel = new VeiculoViewModel
            {
                ClienteId = id,
                NomeCliente = cliente.NomeCompleto,
            };
            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar(VeiculoViewModel viewModel)
        {
            #region[Validação]
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var veiculoEnvio = viewModel.MapearModel();
                _veiculoService.Inserir(veiculoEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction("carregareditar", "clientes", new { id = viewModel.ClienteId });
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult CarregarEditar(int id)
        {
            var veiculo = _veiculoService.Recuperar(id);
            var cliente = _clienteService.Recuperar(veiculo.ClienteId);

            var viewModel = VeiculoViewModel.MapearViewModel(veiculo);
            viewModel.NomeCliente = cliente.NomeCompleto;
            return View("Editar", viewModel);
        }

        [HttpPost]
        [Route("editar")]
        public IActionResult Editar(VeiculoViewModel viewModel)
        {
            #region[Validação]
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var veiculoEnvio = viewModel.MapearModel();
                _veiculoService.Editar(veiculoEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction("carregareditar", "clientes", new { id = viewModel.ClienteId });
        }

    }
}
