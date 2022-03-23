using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tcc.web.Models.API;

namespace tcc.web.Models
{
    public class VeiculoViewModel
    {
        public int ClienteId { get; set; }

        public int? VeiculoId { get; set; }

        [Display(Name = "Cliente")]
        public string NomeCliente { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }

        public static VeiculoViewModel MapearViewModel(VeiculoRetorno veiculo)
        {
            if (veiculo == null) return new VeiculoViewModel();

            var model = new VeiculoViewModel()
            {
                VeiculoId = veiculo.VeiculoId,
                ClienteId = veiculo.ClienteId,
                Placa = veiculo.Placa,
                Marca = veiculo.Marca
            };

            return model;
        }

        public VeiculoEnvio MapearModel()
        {
            return new VeiculoEnvio()
            {
                VeiculoId = this.VeiculoId,
                ClienteId = this.ClienteId,
                Placa = this.Placa,
                Marca = this.Marca
            };
        }
    }

    public class VeiculoGridViewModel
    {
        public int ClienteId { get; set; }

        public int VeiculoId { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }
        public string Situacao { get; set; }

        public static VeiculoGridViewModel MapearViewModel(VeiculoRetorno veiculo)
        {
            if (veiculo == null) return new VeiculoGridViewModel();

            var model = new VeiculoGridViewModel()
            {
                VeiculoId = veiculo.VeiculoId,
                ClienteId = veiculo.ClienteId,
                Placa = veiculo.Placa.ToUpper(),
                Marca = veiculo.Marca.ToUpper(),
                Situacao = veiculo.Ativo ? "Ativo" : "Inativo"
            };

            return model;
        }
    }
}
