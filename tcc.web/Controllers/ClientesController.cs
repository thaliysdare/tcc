using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using tcc.web.Models;
using tcc.web.Models.DTO;

namespace tcc.web.Controllers
{
    [Route("clientes")]
    public class ClientesController : GenericoController
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            var viewModel = new ClientesViewModel();
            var json = client.GetStringAsync("https://localhost:44362/Clientes").Result;
            viewModel.ListaClientes = JsonSerializer.Deserialize<List<ClienteRetorno>>(json).Select(x => ClienteGridViewModel.MapearViewModel(x)).ToList();
            

            return View(viewModel);
        }
    }
}
