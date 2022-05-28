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
    public class OrcamentoController : GenericoController
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IOrcamentoService _orcamentoService;

        public OrcamentoController(IOrcamentoRepository orcamentoRepository, IOrcamentoService orcamentoService)
        {
            _orcamentoRepository = orcamentoRepository;
            _orcamentoService = orcamentoService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<OrcamentoRetornoDTO> GetTodos()
        {
            try
            {
                var retorno = _orcamentoRepository.RecuperarTodos().ToList();
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => OrcamentoRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OrcamentoRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var retorno = _orcamentoRepository.RecuperarPorId(id);
                if (retorno == null) return NotFound();

                return Ok(OrcamentoRetornoDTO.MapearDTO(retorno));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<OrcamentoRetornoDTO> Post([FromBody] OrcamentoEnvioDTO orcamentoEnvioDTO)
        {
            try
            {
                var retornoNovo = _orcamentoService.Inserir(orcamentoEnvioDTO.MapearModel(), orcamentoEnvioDTO.ListaItensServicos);
                return CreatedAtAction(nameof(Get), new { id = retornoNovo.OrcamentoId }, OrcamentoRetornoDTO.MapearDTO(retornoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<OrcamentoRetornoDTO> Put([FromRoute] int id,
                                                     [FromBody] OrcamentoEnvioDTO orcamentoEnvioDTO)
        {
            try
            {
                var model = _orcamentoRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _orcamentoService.Editar(id, orcamentoEnvioDTO.MapearModel(), orcamentoEnvioDTO.ListaItensServicos);
                model = _orcamentoRepository.RecuperarPorId(id);

                return Ok(OrcamentoRetornoDTO.MapearDTO(model));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
