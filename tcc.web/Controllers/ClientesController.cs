using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ClientesController : GenericoController
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService, IUsuarioService usuarioService) : base(usuarioService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ClientesViewModel
            {
                ListaClientes = _clienteService.RecuperarTodos()
                                               .OrderByDescending(x => x.ClienteId)
                                               .Select(x => ClienteGridViewModel.MapearViewModel(x))
                                               .ToList()
            };
            return View(viewModel);
        }

        #region[Cadastrar]
        [HttpGet]
        [Route("cadastrar")]
        public IActionResult CarregarCadastrar(bool modal)
        {
            var viewModel = new ClienteViewModel();

            if (modal)
                return PartialView("Modal/_CadastrarModal", viewModel);

            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public JsonResult Cadastrar(ClienteViewModel viewModel)
        {
            if (ValidarSeEnderecoDigitado(viewModel.EnderecoViewModel))
            {
                if (!ValidarSeTodosCamposEnderecoDigitado(viewModel.EnderecoViewModel))
                    ModelState.AddModelError("ErroEndereco", "Se for informar o endereço todos os campos devem ser preenchidos");
            }
            else viewModel.EnderecoViewModel = null;

            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                var retorno = _clienteService.Inserir(model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST, retorno));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Editar]
        [HttpGet]
        [Route("editar/{id}")]
        public IActionResult CarregarEditar(int id)
        {
            var cliente = _clienteService.Recuperar(id);
            var viewModel = ClienteViewModel.MapearViewModel(cliente);

            if (!cliente.Ativo)
                return View("Inativo", viewModel);
            return View("Editar", viewModel);
        }

        [HttpPost]
        [Route("editar")]
        public JsonResult Editar(ClienteViewModel viewModel)
        {
            if (ValidarSeEnderecoDigitado(viewModel.EnderecoViewModel))
            {
                if (!ValidarSeTodosCamposEnderecoDigitado(viewModel.EnderecoViewModel))
                    ModelState.AddModelError("ErroEndereco", "Se for informar o endereço todos os campos devem ser preenchidos");
            }
            else viewModel.EnderecoViewModel = null;

            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                _clienteService.Editar(viewModel.ClienteId.Value, model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.PUT));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Excluir]
        [HttpGet]
        [Route("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _clienteService.Excluir(id);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region[PartialView]
        [HttpGet]
        [Route("editar/{id}/veiculos")]
        public IActionResult RecuperarVeiculosCliente(int id)
        {
            var cliente = _clienteService.Recuperar(id);
            var viewModel = ClienteViewModel.MapearViewModel(cliente);
            return PartialView("_VeiculosGrid", viewModel);
        }
        #endregion

        #region[JsonResult]
        [HttpGet]
        [Route("json")]
        public JsonResult RecuperarClientes()
        {
            try
            {
                var clientes = _clienteService.RecuperarTodos().Where(x => x.Ativo).ToList();
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, clientes));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Auxiliares]
        private bool ValidarSeEnderecoDigitado(EnderecoViewModel endereco)
        {
            if (!string.IsNullOrEmpty(endereco.Rua)) return true;
            if (!string.IsNullOrEmpty(endereco.Numero)) return true;
            if (!string.IsNullOrEmpty(endereco.Bairro)) return true;
            if (!string.IsNullOrEmpty(endereco.Cidade)) return true;
            if (!string.IsNullOrEmpty(endereco.Estado)) return true;

            return false;
        }

        private bool ValidarSeTodosCamposEnderecoDigitado(EnderecoViewModel endereco)
        {
            if (string.IsNullOrEmpty(endereco.Rua)) return false;
            if (string.IsNullOrEmpty(endereco.Numero)) return false;
            if (string.IsNullOrEmpty(endereco.Bairro)) return false;
            if (string.IsNullOrEmpty(endereco.Cidade)) return false;
            if (string.IsNullOrEmpty(endereco.Estado)) return false;

            return true;
        }
        #endregion
    }
}
