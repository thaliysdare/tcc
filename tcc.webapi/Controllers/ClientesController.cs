using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using tcc.webapi.Models.DTO;
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
        private readonly IVeiculoRepository _veiculoRepository;

        public ClientesController(IClienteRepository clienteRepository,
                                  IClienteService clienteService,
                                  IVeiculoRepository veiculoRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _veiculoRepository = veiculoRepository;
        }

        #region[Cliente]
        [HttpGet]
        public ActionResult<List<ClienteRetornoDTO>> GetListaClientes()
        {
            try
            {
                var clientes = _clienteRepository.RecuperarTodosClientesCompleto();
                if (!clientes.Any())
                {
                    return NotFound();
                }

                var listaClientes = clientes.Select(x => ClienteRetornoDTO.MapearDTO(x)).ToList();
                return Ok(listaClientes);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ClienteRetornoDTO> GetCliente([FromRoute] int id)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarClienteCompleto(id);
                if (cliente == null) return NotFound();

                return ClienteRetornoDTO.MapearDTO(cliente);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<ClienteRetornoDTO> PostCliente([FromBody] ClienteEnvioDTO clienteEnvioDTO)
        {
            try
            {
                var clienteNovo = _clienteService.InserirCliente(clienteEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(GetCliente), new { id = clienteNovo.ClienteId }, ClienteRetornoDTO.MapearDTO(clienteNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ClienteRetornoDTO> PutCliente([FromRoute] int id,
                                                          [FromBody] ClienteEnvioDTO clienteEnvioDTO)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarPorId(id);
                if (cliente == null) return NotFound();

                _clienteService.EditarCliente(id, clienteEnvioDTO.MapearModel());
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCliente([FromRoute] int id)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarPorId(id);
                if (cliente == null) return NotFound();

                _clienteService.InativarCliente(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region[Veiculo]
        [HttpGet]
        [Route("{id}/veiculos")]
        public ActionResult<VeiculoRetornoDTO> GetVeiculosCliente([FromRoute] int id)
        {
            try
            {
                var veiculos = _veiculoRepository.RecuperarListaVeiculosPorCliente(id);
                if (!veiculos.Any()) return NotFound();

                var listaVeiculos = veiculos.Select(x => VeiculoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(listaVeiculos);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}
