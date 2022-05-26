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

        private IQueryable<OrdemServico> RecuperarTodosPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return RecuperarTodos().Where(x => dataInicial <= x.DataSaida && x.DataSaida <= dataFinal);
        }

    }
}
