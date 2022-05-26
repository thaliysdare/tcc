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
    public class OrdemServicoController : GenericoController
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;
        private readonly IOrdemServicoService _ordemServicoService;

        public OrdemServicoController(IOrdemServicoRepository ordemServicoRepository, IOrdemServicoService ordemServicoService)
        {
            _ordemServicoRepository = ordemServicoRepository;
            _ordemServicoService = ordemServicoService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<OrdemServicoRetornoDTO> GetTodos()
        {
            try
            {
                var retorno = _ordemServicoRepository.RecuperarTodos().ToList();
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => OrdemServicoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("finalizados/periodo")]
        public ActionResult<OrdemServicoRetornoDTO> GetTodosFinalizadosPorPeriodo([FromBody] OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            try
            {
                var retorno = _ordemServicoRepository.RecuperarTodosFinalizadosPorPeriodo(ordemServicoEnvioPeriodoDTO);
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => OrdemServicoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("cancelados/periodo")]
        public ActionResult<OrdemServicoRetornoDTO> GetTodosCanceladosPorPeriodo([FromBody] OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            try
            {
                var retorno = _ordemServicoRepository.RecuperarTodosCanceladosPorPeriodo(ordemServicoEnvioPeriodoDTO);
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => OrdemServicoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var retorno = _ordemServicoRepository.RecuperarPorId(id);
                if (retorno == null) return NotFound();

                return Ok(OrdemServicoRetornoDTO.MapearDTO(retorno));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<OrdemServicoRetornoDTO> Post([FromBody] OrdemServicoEnvioDTO ordemServicoEnvioDTO)
        {
            try
            {
                var retornoNovo = _ordemServicoService.Inserir(ordemServicoEnvioDTO.MapearModel(), ordemServicoEnvioDTO.ListaItensServicos);
                return CreatedAtAction(nameof(Get), new { id = retornoNovo.OrdemServicoId }, OrdemServicoRetornoDTO.MapearDTO(retornoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Put([FromRoute] int id,
                                                        [FromBody] OrdemServicoEnvioDTO ordemServicoEnvioDTO)
        {
            try
            {
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Editar(id, ordemServicoEnvioDTO.MapearModel(), ordemServicoEnvioDTO.ListaItensServicos);
                model = _ordemServicoRepository.RecuperarPorId(id);

                return Ok(OrdemServicoRetornoDTO.MapearDTO(model));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
