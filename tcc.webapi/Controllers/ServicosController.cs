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
    public class ServicosController : GenericoController
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IServicoService _servicoService;

        public ServicosController(IServicoRepository servicoRepository, IServicoService servicoService)
        {
            _servicoRepository = servicoRepository;
            _servicoService = servicoService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<ServicoRetornoDTO> GetTodos()
        {
            try
            {
                var retorno = _servicoRepository.RecuperarTodos();
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => ServicoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ServicoRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var retorno = _servicoRepository.RecuperarPorId(id);
                if (retorno == null) return NotFound();

                return Ok(ServicoRetornoDTO.MapearDTO(retorno));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<ServicoRetornoDTO> Post([FromBody] ServicoEnvioDTO servicoEnvioDTO)
        {
            try
            {
                var retornoNovo = _servicoService.Inserir(servicoEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(Get), new { id = retornoNovo.ServicoId }, ServicoRetornoDTO.MapearDTO(retornoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<ServicoRetornoDTO> Put([FromRoute] int id,
                                                   [FromBody] ServicoEnvioDTO servicoEnvioDTO)
        {
            try
            {
                var model = _servicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _servicoService.Editar(id, servicoEnvioDTO.MapearModel());
                return Ok(ServicoRetornoDTO.MapearDTO(servicoEnvioDTO.MapearModel()));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                var model = _servicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _servicoService.Inativar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
