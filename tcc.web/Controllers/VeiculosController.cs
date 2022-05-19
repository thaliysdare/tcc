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
    public class VeiculosController : GenericoController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IClienteService _clienteService;
        public VeiculosController(IVeiculoService veiculoService,
                                  IClienteService clienteService,
                                  IUsuarioService usuarioService) : base(usuarioService)
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
        public IActionResult CarregarCadastrar(int id, bool modal)
        {
            var cliente = _clienteService.Recuperar(id);
            var viewModel = new VeiculoViewModel
            {
                ClienteId = id,
                NomeCliente = cliente.NomeCompleto,
            };

            if (modal)
                return PartialView("Modal/_CadastrarModal", viewModel);
            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public JsonResult Cadastrar(VeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                var retorno = _veiculoService.Inserir(model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST, retorno));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult CarregarEditar(int id, bool modal)
        {
            var veiculo = _veiculoService.Recuperar(id);
            var cliente = _clienteService.Recuperar(veiculo.ClienteId);

            var viewModel = VeiculoViewModel.MapearViewModel(veiculo);
            viewModel.NomeCliente = cliente.NomeCompleto;

            if (modal)
                return PartialView("Modal/_EditarModal", viewModel);
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
                _veiculoService.Editar(veiculoEnvio.VeiculoId.Value, veiculoEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction("carregareditar", "clientes", new { id = viewModel.ClienteId });
        }

        [HttpGet]
        [Route("json")]
        public JsonResult RecuperarVeiculos()
        {
            try
            {
                var veiculos = _veiculoService.RecuperarTodos().Where(x => x.Ativo).ToList();
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, veiculos));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpGet]
        [Route("cliente/{id}/json")]
        public JsonResult RecuperarVeiculosDoCliente(int id)
        {
            try
            {
                var veiculos = _veiculoService.RecuperarTodos().Where(x => x.Ativo && x.ClienteId == id).ToList();
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, veiculos));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

    }
}
