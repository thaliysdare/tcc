using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using tcc.web.Models;
using tcc.web.Models.API;
using tcc.web.Services.IService;
using tcc.web.Utils;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class OrcamentoController : GenericoController
    {
        private readonly IOrcamentoService _orcamentoService;
        private readonly IClienteService _clienteService;
        private readonly IVeiculoService _veiculoService;
        private readonly IServicoService _servicoService;
        public OrcamentoController(IOrcamentoService orcamentoService,
                                   IClienteService clienteService,
                                   IVeiculoService veiculoService,
                                   IUsuarioService usuarioService,
                                   IServicoService servicoService) : base(usuarioService)
        {
            _orcamentoService = orcamentoService;
            _clienteService = clienteService;
            _veiculoService = veiculoService;
            _servicoService = servicoService;
        }

        #region[Index]
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new OrcamentosViewModel()
            {
                ListaOrcamento = _orcamentoService.RecuperarTodos()
                                              .OrderByDescending(x => x.OrcamentoId)
                                              .Select(x => OrcamentoGridViewModel.MapearViewModel(x))
                                              .ToList()
            };
            return View(viewModel);
        }
        #endregion

        #region[Cadastrar]
        [HttpGet]
        [Route("cadastrar")]
        [Authorize(Roles = "NV2")]
        public IActionResult CarregarCadastrar()
        {
            var usuario = RecuperarUsuarioLogado();
            var viewModel = new OrcamentoViewModel()
            {
                UsuarioId = usuario.UsuarioId,
                UsuarioNome = usuario.Nome,
                DataPrevisao = DateTime.Now.AddDays(1),
            };

            return View("Cadastrar", viewModel);
        }

        [HttpPost]
        [Route("cadastrar")]
        [Authorize(Roles = "NV2")]
        public JsonResult Cadastrar([FromBody] OrcamentoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                model.ListaItensServicos = _servicoService.RecuperarApenasAtivos()
                                                          .Where(x => viewModel.ListaServicosEscolhidosId.Contains(x.ServicoId))
                                                          .Select(x => new ServicoOrcamentoEnvio()
                                                          {
                                                              ServicoId = x.ServicoId,
                                                              Valor = x.Valor

                                                          }).ToList();

                var orcamento = _orcamentoService.Inserir(model);
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST, orcamento));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Editar]
        [HttpGet]
        [Route("editar/{id}")]
        [Authorize(Roles = "NV1")]
        public IActionResult CarregarEditar(int id)
        {
            var model = _orcamentoService.Recuperar(id);
            var viewModel = OrcamentoViewModel.MapearViewModel(model);

            if (model.Situacao == OrcamentoSituacaoEnum.OSGerada)
                return View("Cancelada", viewModel);

            return View("Editar", viewModel);
        }

        [HttpPut]
        [Route("editar")]
        [Authorize(Roles = "NV1")]
        public JsonResult Editar([FromBody] OrcamentoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            try
            {
                var model = viewModel.MapearModel();
                _orcamentoService.Editar(model.OrcamentoId.Value, model);

                return Json(PrepararJsonRetorno(GenericoJsonRetorno.PUT, model));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Imprimir]
        [HttpGet]
        [Route("imprimir/{id}")]
        [Authorize(Roles = "NV1")]
        public IActionResult CarregarImprimir(int id)
        {
            var model = _orcamentoService.Recuperar(id);
            var viewModel = OrcamentoViewModel.MapearViewModel(model);
            return View("Imprimir", viewModel);
        }
        #endregion

        #region[PartialView]
        [HttpGet]
        [Route("clientes")]
        public PartialViewResult RecupearListaClientes()
        {
            var viewModel = new OrcamentoViewModel
            {
                ListaClientes = _clienteService.RecuperarTodos()
                                               .Where(x => x.Ativo)
                                               .Select(x => new SelectListItem()
                                               {
                                                   Text = x.NomeCompleto,
                                                   Value = x.ClienteId.ToString(),
                                               }).ToList()
            };
            return PartialView("_SelecaoClientes", viewModel);
        }

        [HttpGet]
        [Route("veiculos/{id}")]
        public PartialViewResult RecupearListaVeiculosPorCliente(int id)
        {
            var viewModel = new OrcamentoViewModel
            {
                ClienteId = id,
                ListaVeiculos = _veiculoService.RecuperarTodos()
                                               .Where(x => x.Ativo && x.ClienteId == id)
                                               .Select(x => new SelectListItem()
                                               {
                                                   Text = x.Placa,
                                                   Value = x.VeiculoId.ToString(),
                                               }).ToList()
            };
            return PartialView("_SelecaoVeiculos", viewModel);
        }

        [HttpGet]
        [Route("servicos")]
        public PartialViewResult RecarregarServicos(bool checkbox)
        {
            var viewModel = _servicoService.RecuperarApenasAtivos()
                                           .Select(x => OrcamentoServicosViewModel.MapearViewModel(x))
                                           .ToList();
            if (checkbox)
                return PartialView("_ServicosCheckboxGrid", viewModel);

            return PartialView("_ServicosGrid", viewModel);
        }

        [HttpGet]
        [Route("servicos-os/{id}")]
        [Route("servicos-os/{id}/{imprimir}")]
        public PartialViewResult RecarregarServicosOS(int id, bool? imprimir = null)
        {
            var model = _orcamentoService.Recuperar(id);
            var viewModel = model.ListaItensServicos.Select(x => OrcamentoItensViewModel.MapearViewModel(x)).ToList();

            if (model.Situacao == OrcamentoSituacaoEnum.OSGerada
                || (imprimir.HasValue && imprimir.Value))
                return PartialView("_ServicosOrcamentoCanceladaGrid", viewModel);

            return PartialView("_ServicosOrcamentoGrid", viewModel);
        }
        #endregion

        #region[JsonResult]
        [HttpGet]
        [Route("servicos/json")]
        public JsonResult RecuperarServicos()
        {
            try
            {
                var servicos = _servicoService.RecuperarApenasAtivos()
                                              .Select(x => OrcamentoServicosViewModel.MapearViewModel(x))
                                              .ToList();
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, servicos));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpGet]
        [Route("servicos-os/{id}/json")]
        public JsonResult RecuperarServicosOS(int id)
        {
            try
            {
                var servicos = _orcamentoService.Recuperar(id)
                                                   .ListaItensServicos
                                                   .Select(x => OrcamentoItensViewModel.MapearViewModel(x))
                                                   .ToList();
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, servicos));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpPost]
        [Route("servicos-os")]
        public JsonResult RegistrarServicosOS(int orcamentoId, int servicoId)
        {
            try
            {
                var orcamento = _orcamentoService.Recuperar(orcamentoId);
                if (orcamento.ListaItensServicos == null) orcamento.ListaItensServicos = new List<ServicoOrcamentoRetorno>();
                if (orcamento.ListaItensServicos.Any(x => x.ServicoId == servicoId)) throw new Exception("Serviço já adicionado a OS");

                var servico = _servicoService.Recuperar(servicoId);
                if (servico == null) throw new Exception("Serviço não encontrado");

                var viewModel = OrcamentoViewModel.MapearViewModel(orcamento);
                viewModel.ListaItens.Add(new OrcamentoItensViewModel
                {
                    OrcamentoId = orcamento.OrcamentoId,
                    ServicoId = servicoId,
                    Valor = servico.Valor
                });

                var model = viewModel.MapearModel();
                _orcamentoService.Editar(model.OrcamentoId.Value, model);
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }

        [HttpPut]
        [Route("servicos-os")]
        public JsonResult AtualizarServicosOS(int orcamentoId, int servicoOrcamentoId, double? valor = null)
        {
            try
            {
                var orcamento = _orcamentoService.Recuperar(orcamentoId);
                if (orcamento.ListaItensServicos == null) orcamento.ListaItensServicos = new List<ServicoOrcamentoRetorno>();

                var serviocOrcamento = orcamento.ListaItensServicos.FirstOrDefault(x => x.ServicoOrcamentoId == servicoOrcamentoId);
                if (serviocOrcamento == null) throw new Exception("Serviço não encontrado na OS");

                if (valor.HasValue) serviocOrcamento.Valor = valor.Value;
                else orcamento.ListaItensServicos.Remove(serviocOrcamento);

                var viewModel = OrcamentoViewModel.MapearViewModel(orcamento);
                var model = viewModel.MapearModel();
                _orcamentoService.Editar(model.OrcamentoId.Value, model);
                return Json(PrepararJsonRetorno(GenericoJsonRetorno.POST));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErroServidor", e.Message);
                return Json(PrepararJsonRetornoErro());
            }
        }
        #endregion

        #region[Auxiliares]
        private OrcamentoViewModel MontagemSelectListClienteVeiculo(OrcamentoViewModel viewModel)
        {
            var clientes = _clienteService.RecuperarTodos().Where(x => x.Ativo).ToList();
            if (clientes.Any())
            {
                var cliente = clientes.FirstOrDefault();
                viewModel.ListaClientes = clientes.Select(x => new SelectListItem()
                {
                    Text = x.NomeCompleto,
                    Value = x.ClienteId.ToString(),
                    Selected = x.ClienteId == cliente.ClienteId
                }).ToList();

                var veiculos = _veiculoService.RecuperarTodos().Where(x => x.ClienteId == cliente.ClienteId).ToList();
                if (veiculos.Any())
                {
                    var veiculo = veiculos.FirstOrDefault();
                    viewModel.ListaVeiculos = veiculos.Select(x => new SelectListItem()
                    {
                        Text = x.Placa,
                        Value = x.VeiculoId.ToString(),
                        Selected = x.VeiculoId == veiculo.VeiculoId
                    }).ToList();
                }
            }
            return viewModel;
        }
        #endregion
    }
}
