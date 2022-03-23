using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tcc.web.Models.API;

namespace tcc.web.Models
{
    public class ServicosViewModel
    {
        public List<ServicoGridViewModel> ListaServicos { get; set; }

        public ServicosViewModel()
        {
            ListaServicos = new List<ServicoGridViewModel>();
        }
    }

    public class ServicoViewModel
    {
        public int? ServicoId { get; set; }

        [Required(ErrorMessage = "Favor informar a descrição completa")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Favor informar a descrição resumida")]
        [Display(Name = "Descrição resumida")]
        public string DescricaoResumida { get; set; }

        [Required(ErrorMessage = "Favor informar o valor do serviço")]
        [Display(Name = "Valor do serviço")]
        public double Valor { get; set; }

        public bool Ativo { get; set; }

        public static ServicoViewModel MapearViewModel(ServicoRetorno servico)
        {
            if (servico == null) return new ServicoViewModel();

            var model = new ServicoViewModel()
            {
                ServicoId = servico.ServicoId,
                Descricao = servico.Descricao,
                DescricaoResumida = servico.DescricaoResumida,
                Valor = servico.Valor,
                Ativo = servico.Ativo,
            };

            return model;
        }

        public ServicoEnvio MapearModel()
        {
            return new ServicoEnvio()
            {
                ServicoId = this.ServicoId,
                Descricao = this.Descricao,
                DescricaoResumida = this.DescricaoResumida,
                Valor = this.Valor
            };
        }
    }

    public class ServicoGridViewModel
    {
        public int ServicoId { get; set; }
        public string Descricao { get; set; }
        public string DescricaoResumida { get; set; }
        public double Valor { get; set; }
        public string Situacao { get; set; }

        public static ServicoGridViewModel MapearViewModel(ServicoRetorno servico)
        {
            if (servico == null) return new ServicoGridViewModel();

            var model = new ServicoGridViewModel()
            {
                ServicoId = servico.ServicoId,
                Descricao = servico.Descricao,
                DescricaoResumida = servico.DescricaoResumida,
                Valor = servico.Valor,
                Situacao = servico.Ativo ? "Ativo" : "Inativo"
            };

            return model;
        }
    }
}
