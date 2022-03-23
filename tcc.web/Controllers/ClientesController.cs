using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    public class ClientesController : GenericoController
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ClientesViewModel
            {
                ListaClientes = _clienteService.RecuperarTodos()
                                               .Select(x => ClienteGridViewModel.MapearViewModel(x))
                                               .ToList()
            };
            return View(viewModel);
        }

        #region[Cadastrar]
        [HttpGet]
        [Route("cadastrar")]
        public IActionResult CarregarCadastrar()
        {
            var viewModel = new ClienteViewModel();
            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar(ClienteViewModel viewModel)
        {
            #region[Validação]
            if (ValidarSeEnderecoDigitado(viewModel.EnderecoViewModel))
            {
                if (!ValidarSeTodosCamposEnderecoDigitado(viewModel.EnderecoViewModel))
                    ModelState.AddModelError("ErroEndereco", "Se for informar o endereço todos os campos devem ser preenchidos");
            }
            else viewModel.EnderecoViewModel = null;
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var clienteEnvio = viewModel.MapearModel();
                _clienteService.Inserir(clienteEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
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
        public IActionResult Editar(ClienteViewModel viewModel)
        {
            #region[Validação]
            if (ValidarSeEnderecoDigitado(viewModel.EnderecoViewModel))
            {
                if (!ValidarSeTodosCamposEnderecoDigitado(viewModel.EnderecoViewModel))
                    ModelState.AddModelError("ErroEndereco", "Se for informar o endereço todos os campos devem ser preenchidos");
            }
            else viewModel.EnderecoViewModel = null;
            #endregion

            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var clienteEnvio = viewModel.MapearModel();
                _clienteService.Editar(clienteEnvio.ClienteId.Value, clienteEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
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
