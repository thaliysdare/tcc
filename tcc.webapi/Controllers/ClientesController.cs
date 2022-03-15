using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : GenericoController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteRepository clienteRepository, IClienteService clienteService)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteRetornoDTO>> GetTodosItens()
        {
            try
            {
                var clientes = _clienteRepository.RecuperarTodos();
                if (!clientes.Any())
                {
                    return NotFound();
                }
                return clientes.Select(x => ClienteRetornoDTO.MapearDTO(x)).ToList();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ClienteRetornoDTO> GetItem(int id)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarPorId(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                var clienteDTO = ClienteRetornoDTO.MapearDTO(cliente);
                if (cliente.Endereco != null)
                    clienteDTO.Endereco = EnderecoRetornoDTO.MapearDTO(cliente.Endereco);
                return clienteDTO;
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<ClienteRetornoDTO> PostItem(ClienteEnvioDTO clienteEnvioDTO)
        {
            try
            {
                var clienteNovo = _clienteService.Inserir(clienteEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(GetItem), new { id = clienteNovo.ClienteId }, ClienteRetornoDTO.MapearDTO(clienteNovo));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteItem(int id)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarPorId(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                _clienteService.Excluir(cliente);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
