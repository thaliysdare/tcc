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
    public class HealthcheckController : GenericoController
    {
        private readonly IClienteRepository _clienteRepository;

        public HealthcheckController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("verificacao")]
        public ActionResult<VeiculoRetornoDTO> API()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("verificacao-banco")]
        public ActionResult<VeiculoRetornoDTO> Banco()
        {
            try
            {
                var veiculos = _clienteRepository.RecuperarTodos().ToList();
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
