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
    public class UsuariosController : GenericoController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<UsuarioRetornoDTO> GetTodos()
        {
            try
            {
                var retorno = _usuarioRepository.RecuperarTodos().ToList();
                if (!retorno.Any()) return NotFound();

                var retornoDTO = retorno.Select(x => UsuarioRetornoDTO.MapearDTO(x)).ToList();
                return Ok(retornoDTO);
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<UsuarioRetornoDTO> Get([FromRoute] int id)
        {
            try
            {
                var retorno = _usuarioRepository.RecuperarPorId(id);
                if (retorno == null) return NotFound();

                return Ok(UsuarioRetornoDTO.MapearDTO(retorno));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<UsuarioRetornoDTO> Post([FromBody] UsuarioEnvioDTO usuarioEnvioDTO)
        {
            try
            {
                var retornoNovo = _usuarioService.Inserir(usuarioEnvioDTO.MapearModel());
                return CreatedAtAction(nameof(Get), new { id = retornoNovo.UsuarioId }, UsuarioRetornoDTO.MapearDTO(retornoNovo));
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<UsuarioRetornoDTO> Put([FromRoute] int id,
                                                   [FromBody] UsuarioEnvioDTO usuarioEnvioDTO)
        {
            try
            {
                var model = _usuarioRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _usuarioService.Editar(id, usuarioEnvioDTO.MapearModel());
                return Ok(UsuarioRetornoDTO.MapearDTO(usuarioEnvioDTO.MapearModel()));
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
                var model = _usuarioRepository.RecuperarPorId(id);
                if (model == null) return NotFound();

                _usuarioService.Inativar(id);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
