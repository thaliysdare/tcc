using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Repositories;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : GenericoController
    {
        private readonly IClienteRepository _clienteRepository;
        
        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _clienteRepository.RecuperarTodos();
        }
    }
}
