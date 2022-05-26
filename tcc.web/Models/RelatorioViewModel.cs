using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tcc.web.Models
{
    public class RelatorioViewModel
    {
        [Required(ErrorMessage = "Favor informar data inical do período")]
        [Display(Name = "Data inicial")]
        public DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "Favor informar data final do período")]
        [Display(Name = "Data final")]
        public DateTime DataFinal { get; set; }

        public List<ServicosExecutadosPeriodoVieWModel> ListaServicosExecutadosPeriodo { get; set; }
        public List<OSGeradasPeriodoViewModel> ListaOSGeradasPeriodo { get; set; }

        public RelatorioViewModel()
        {
            this.ListaServicosExecutadosPeriodo = new List<ServicosExecutadosPeriodoVieWModel>();
            this.ListaOSGeradasPeriodo = new List<OSGeradasPeriodoViewModel>();
        }
    }

    public class ServicosExecutadosPeriodoVieWModel
    {
        public int QtdVezes { get; set; }
        public string DescricaoServico { get; set; }
        public double MediaValor { get; set; }
    }

    public class OSGeradasPeriodoViewModel
    {
        public string QtdOS { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
        public string CorCard { get; set; }
        public string CorTexto { get; set; }
        public string IdCard { get; set; }
    }
}
