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
    public class FuncionalidadesController : GenericoController
    {
        private readonly IFuncionalidadeRepository _funcionalidadeRepository;
        private readonly IFuncionalidadeService _funcionalidadeService;

        public FuncionalidadesController(IFuncionalidadeRepository funcionalidadeRepository, IFuncionalidadeService funcionalidadeService)
        {
            _funcionalidadeRepository = funcionalidadeRepository;
            _funcionalidadeService = funcionalidadeService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<FuncionalidadeRetornoDTO> GetTodos()
        {
            try
            {
                var retorno = _funcionalidadeRepository.RecuperarTodos().ToList();
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => FuncionalidadeRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<FuncionalidadeRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var retorno = _funcionalidadeRepository.RecuperarPorId(id);
                if (retorno == null) return NotFound();

                return Ok(FuncionalidadeRetornoDTO.MapearDTO(retorno));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<FuncionalidadeRetornoDTO> Post([FromBody] FuncionalidadeEnvioDTO FuncionalidadeEnvioDTO)
        {
            try
            {
                var retornoNovo = _funcionalidadeService.Inserir(FuncionalidadeEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(Get), new { id = retornoNovo.FuncionalidadeId }, FuncionalidadeRetornoDTO.MapearDTO(retornoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<FuncionalidadeRetornoDTO> Put([FromRoute] int id,
                                                          [FromBody] FuncionalidadeEnvioDTO FuncionalidadeEnvioDTO)
        {
            try
            {
                var model = _funcionalidadeRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _funcionalidadeService.Editar(id, FuncionalidadeEnvioDTO.MapearModel());
                return Ok(FuncionalidadeRetornoDTO.MapearDTO(FuncionalidadeEnvioDTO.MapearModel()));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
