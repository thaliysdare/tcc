﻿using Microsoft.AspNetCore.Mvc;
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
                var retorno = _ordemServicoRepository.RecuperarTodosOrdemServicoCompleto();
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
                var retorno = _ordemServicoRepository.RecuperarOrdemServicoCompleto(id);
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
                var retornoNovo = _ordemServicoService.Inserir(ordemServicoEnvioDTO.MapearModel(), ordemServicoEnvioDTO.ListaServicos);
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

                _ordemServicoService.Editar(id, ordemServicoEnvioDTO.MapearModel(), ordemServicoEnvioDTO.ListaServicos);
                return Ok(OrdemServicoRetornoDTO.MapearDTO(ordemServicoEnvioDTO.MapearModel()));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("iniciar-os/{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Iniciar([FromRoute] int id)
        {
            try
            {
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Iniciar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("reiniciar-os/{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Reiniciar([FromRoute] int id)
        {
            try
            {
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Reiniciar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("paralizar-os/{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Paralizar([FromRoute] int id, [FromBody] string motivo)
        {
            try
            {
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Paralizar(id, motivo);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("finalizar-os/{id}")]
        public ActionResult<OrdemServicoRetornoDTO> Finalizar([FromRoute] int id)
        {
            try
            {
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Finalizar(id);
                return Ok();
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
                var model = _ordemServicoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _ordemServicoService.Cancelar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
