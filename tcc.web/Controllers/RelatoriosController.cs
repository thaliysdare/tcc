using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tcc.web.Models;
using tcc.web.Services.IService;

namespace tcc.web.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "NV4")]
    public class RelatoriosController : GenericoController
    {
        private readonly IOrdemServicoService _ordemServicoService;
        private readonly IOrcamentoService _orcamentoService;

        public RelatoriosController(IOrdemServicoService ordemServicoService,
                                    IUsuarioService usuarioService, 
                                    IOrcamentoService orcamentoService) : base(usuarioService)
        {
            this._ordemServicoService = ordemServicoService;
            this._orcamentoService = orcamentoService;

        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new RelatorioViewModel();
            viewModel.DataInicial = DateTime.Now.AddDays(-1);
            viewModel.DataFinal = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        [Route("periodo/barra/{qtdLimite}")]
        [Route("periodo/barra")]
        public JsonResult CarregarRelatorioBarraPeriodo(RelatorioViewModel viewModel, int? qtdLimite = null)
        {
            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            viewModel.ListaServicosExecutadosPeriodo = _ordemServicoService.RecuperarTodosFinalizadosPorPeriodo(viewModel.DataInicial, viewModel.DataFinal)
                                                                           .SelectMany(x => x.ListaItensServicos)
                                                                           .GroupBy(x => x.ServicoId)
                                                                           .Select(x => new ServicosExecutadosPeriodoVieWModel()
                                                                           {
                                                                               QtdVezes = x.Count(),
                                                                               DescricaoServico = x.FirstOrDefault().Servico.Descricao,
                                                                               MediaValor = x.Average(y => y.Valor)
                                                                           }).ToList();

            if(qtdLimite != null)
            {
                viewModel.ListaServicosExecutadosPeriodo = viewModel.ListaServicosExecutadosPeriodo.OrderByDescending(x => x.QtdVezes).Take(qtdLimite.Value).ToList();
            }

            return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, viewModel));
        }

        [HttpPost]
        [Route("periodo/card")]
        public JsonResult CarregarRelatorioCardPeriodo(RelatorioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(PrepararJsonRetornoErro());

            var totalOSSucesso = _ordemServicoService.RecuperarTodosFinalizadosPorPeriodo(viewModel.DataInicial, viewModel.DataFinal);
            viewModel.ListaOSGeradasPeriodo.Add(new OSGeradasPeriodoViewModel()
            {
                QtdOS = totalOSSucesso.Count().ToString(),
                Valor = totalOSSucesso.Sum(x => x.ValorOrdemServico).ToString(),
                Descricao = "Total OS fechadas com sucesso no período",
                CorCard = "bg-success",
                CorTexto = "text-white",
                IdCard = "cardOSSucesso"
            });

            var totalOrcamentoSemOS = _orcamentoService.RecuperarTodosSemOSPorPeriodo(viewModel.DataInicial, viewModel.DataFinal);
            viewModel.ListaOSGeradasPeriodo.Add(new OSGeradasPeriodoViewModel()
            {
                QtdOS = totalOrcamentoSemOS.Count().ToString(),
                Valor = totalOrcamentoSemOS.Sum(x => x.ValorOrcamento).ToString(),
                Descricao = "Total orçamentos ainda sem OS gerado no período",
                CorCard = "bg-warning",
                CorTexto = "text-white",
                IdCard = "cardOrcamentoSemOS"
            });

            var totalOSCanceladas = _ordemServicoService.RecuperarTodosCanceladosPorPeriodo(viewModel.DataInicial, viewModel.DataFinal);
            viewModel.ListaOSGeradasPeriodo.Add(new OSGeradasPeriodoViewModel()
            {
                QtdOS = totalOSCanceladas.Count().ToString(),
                Valor = totalOSCanceladas.Sum(x => x.ValorOrdemServico).ToString(),
                Descricao = "Total OS canceladas no período",
                CorCard = "bg-danger",
                CorTexto = "text-white",
                IdCard = "cardOSCanceladas"
            });

            viewModel.ListaOSGeradasPeriodo.Add(new OSGeradasPeriodoViewModel()
            {
                QtdOS = (totalOSSucesso.Count() + totalOSCanceladas.Count()).ToString(),
                Valor = (totalOSSucesso.Sum(x => x.ValorOrdemServico) + totalOSCanceladas.Sum(x => x.ValorOrdemServico)).ToString(),
                Descricao = "Total OS geradas no período",
                CorCard = "bg-light",
                CorTexto = "text-black",
                IdCard = "cardOSGeradas"
            });

            return Json(PrepararJsonRetorno(GenericoJsonRetorno.GET, viewModel));
        }
    }
}
