using System;
using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class OrcamentoRepository : GenericoRepository<Orcamento>, IOrcamentoRepository
    {
        public OrcamentoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }

        public List<Orcamento> RecuperarTodosComOSGeradaPorPeriodo(OrcamentoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            return RecuperarTodosPorPeriodo(ordemServicoEnvioPeriodoDTO.DataInicial, ordemServicoEnvioPeriodoDTO.DataFinal)
                   .Where(x => x.IdcStatusOrcamento == Enums.StatusOrcamentoEnum.OSGerada)
                   .ToList();
        }

        public List<Orcamento> RecuperarTodosSemOSPorPeriodo(OrcamentoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            return RecuperarTodosPorPeriodo(ordemServicoEnvioPeriodoDTO.DataInicial, ordemServicoEnvioPeriodoDTO.DataFinal)
                   .Where(x => x.IdcStatusOrcamento == Enums.StatusOrcamentoEnum.OrcamentoGerado)
                   .ToList();
        }

        private IQueryable<Orcamento> RecuperarTodosPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var dtIni = new DateTime(dataInicial.Year, dataInicial.Month, dataInicial.Day, 0, 0, 0);
            var dtFim = new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day, 23, 59, 59);

            return RecuperarTodos().Where(x => dtIni <= x.DataOrcamento && x.DataOrcamento <= dtFim);
        }

    }
}
