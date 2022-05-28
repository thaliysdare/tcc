using System;
using System.Collections.Generic;
using System.Linq;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Models.DTO;
using tcc.webapi.Repositories.IRepositories;

namespace tcc.webapi.Repositories
{
    public class OrdemServicoRepository : GenericoRepository<OrdemServico>, IOrdemServicoRepository
    {
        public OrdemServicoRepository(BancoContexto bancoContexto) : base(bancoContexto)
        {
        }

        public List<OrdemServico> RecuperarTodosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            return RecuperarTodosPorPeriodo(ordemServicoEnvioPeriodoDTO.DataInicial, ordemServicoEnvioPeriodoDTO.DataFinal, true)
                   .ToList();
        }

        public List<OrdemServico> RecuperarTodosFinalizadosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            return RecuperarTodosPorPeriodo(ordemServicoEnvioPeriodoDTO.DataInicial, ordemServicoEnvioPeriodoDTO.DataFinal)
                   .Where(x => x.IdcStatusOrdemServico == Enums.StatusOrdemServicoEnum.OSFinalizada)
                   .ToList();
        }

        public List<OrdemServico> RecuperarTodosCanceladosPorPeriodo(OrdemServicoEnvioPeriodoDTO ordemServicoEnvioPeriodoDTO)
        {
            return RecuperarTodosPorPeriodo(ordemServicoEnvioPeriodoDTO.DataInicial, ordemServicoEnvioPeriodoDTO.DataFinal)
                   .Where(x => x.IdcStatusOrdemServico == Enums.StatusOrdemServicoEnum.OSCancelada)
                   .ToList();
        }

        private IQueryable<OrdemServico> RecuperarTodosPorPeriodo(DateTime dataInicial, DateTime dataFinal, bool todos = false)
        {
            var dtIni = new DateTime(dataInicial.Year, dataInicial.Month, dataInicial.Day, 0, 0, 0);
            var dtFim = new DateTime(dataFinal.Year, dataFinal.Month, dataFinal.Day, 23, 59, 59);

            if (todos)
                return RecuperarTodos().Where(x => dtIni <= x.DataEntrada && x.DataEntrada <= dtFim);

            return RecuperarTodos().Where(x => dtIni <= x.DataSaida && x.DataSaida <= dtFim);
        }

    }
}
