using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tcc.web.Models;
using tcc.web.Models.DTO;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("clientes")]
    public class ClientesController : GenericoController
    {
        private readonly IClienteService _clienteService;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientesController(IHttpClientFactory httpClientFactory, IClienteService clienteService)
        {
            _httpClientFactory = httpClientFactory;
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ClientesViewModel();
            viewModel.ListaClientes = _clienteService.RecuperarClientes().Result.Where(x => x.Ativo).Select(x => ClienteGridViewModel.MapearViewModel(x)).ToList();
            return View(viewModel);
        }

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
                _clienteService.InserirCliente(clienteEnvio);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }

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
