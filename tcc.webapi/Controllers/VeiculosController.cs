using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculosController : GenericoController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculosController(IClienteRepository clienteRepository,
                                  IClienteService clienteService,
                                  IVeiculoRepository veiculoRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<VeiculoRetornoDTO> GetTodos()
        {
            try
            {
                var veiculos = _veiculoRepository.RecuperarTodos();
                if (!veiculos.Any()) return NotFound();

                var listaVeiculos = veiculos.Select(x => VeiculoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(listaVeiculos);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<VeiculoRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var veiculo = _veiculoRepository.RecuperarPorId(id);
                if (veiculo == null) return NotFound();

                return Ok(VeiculoRetornoDTO.MapearDTO(veiculo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<VeiculoRetornoDTO> Post([FromBody] VeiculoEnvioDTO veiculoEnvioDTO)
        {
            try
            {
                var veiculoNovo = _clienteService.InserirVeiculo(veiculoEnvioDTO.ClienteId, veiculoEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(Get), new { id = veiculoNovo.VeiculoId }, VeiculoRetornoDTO.MapearDTO(veiculoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<VeiculoRetornoDTO> Put([FromRoute] int id,
                                                          [FromBody] VeiculoEnvioDTO veiculoEnvioDTO)
        {
            try
            {
                var cliente = _clienteRepository.RecuperarPorId(veiculoEnvioDTO.ClienteId);
                if (cliente == null) return NotFound();

                var veiculo = _veiculoRepository.RecuperarPorId(id);
                if (veiculo == null) return NotFound();

                _clienteService.EditarVeiculo(cliente.ClienteId, id, veiculoEnvioDTO.MapearModel());
                return Ok(VeiculoRetornoDTO.MapearDTO(veiculoEnvioDTO.MapearModel()));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int veiculoId)
        {
            try
            {
                var veiculo = _veiculoRepository.RecuperarPorId(veiculoId);
                if (veiculo == null) return NotFound();

                _clienteService.InativarVeiculo(veiculo.ClienteId, veiculoId);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
