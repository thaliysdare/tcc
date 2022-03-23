using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tcc.web.Models.DTO;
using tcc.web.Utils;

namespace tcc.web.Models
{
    public class ClientesViewModel
    {
        public List<ClienteGridViewModel> ListaClientes { get; set; }

        public ClientesViewModel()
        {
            ListaClientes = new List<ClienteGridViewModel>();
        }
    }

    public class ClienteViewModel
    {
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "Favor informar o nome do cliente")]
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        [Display(Name = "CPF / CNPJ")]
        public string CPFOuCNPJ { get; set; }

        [Required(ErrorMessage = "Favor informar o telefone do cliente")]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        public EnderecoViewModel EnderecoViewModel { get; set; }

        public ClienteViewModel()
        {
            EnderecoViewModel = new EnderecoViewModel();
        }

        public static ClienteViewModel MapearViewModel(ClienteRetorno cliente)
        {
            var model = new ClienteViewModel()
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                CPFOuCNPJ = cliente.CPFOuCNPJ,
                Telefone1 = cliente.Telefone1,
                Telefone2 = cliente.Telefone2,
                EnderecoViewModel = EnderecoViewModel.MapearViewModel(cliente.Endereco)
            };
            return model;
        }

        public ClienteEnvio MapearModel()
        {
            return new ClienteEnvio()
            {
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                CPFOuCNPJ = this.CPFOuCNPJ,
                Telefone1 = this.Telefone1,
                Telefone2 = this.Telefone2,
                Endereco = this.EnderecoViewModel != null ? this.EnderecoViewModel.MapearModel() : default(EnderecoEnvio)
            };
        }
    }

    public class ClienteGridViewModel
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPFOuCNPJ { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        public static ClienteGridViewModel MapearViewModel(ClienteRetorno cliente)
        {
            var viewModel = new ClienteGridViewModel()
            {
                ClienteId = cliente.ClienteId,
                Nome = $"{cliente.Nome} {cliente.Sobrenome}",
                CPFOuCNPJ = cliente.CPFOuCNPJ.FormatarCPFOuCNPJ(),
            };

            viewModel.Telefone = cliente.Telefone1.FormatarContato();
            if (!string.IsNullOrEmpty(cliente.Telefone2)) viewModel.Telefone = viewModel.Telefone + " - " + cliente.Telefone2.FormatarContato();

            if (cliente.Endereco != null)
            {
                var endereco = new StringBuilder();
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Rua) ? string.Empty : cliente.Endereco.Rua + " - ");
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Numero) ? string.Empty : cliente.Endereco.Numero + " - ");
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Complemento) ? string.Empty : cliente.Endereco.Complemento + " - ");
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Bairro) ? string.Empty : cliente.Endereco.Bairro + " - ");
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Cidade) ? string.Empty : cliente.Endereco.Cidade + " - ");
                endereco.Append(string.IsNullOrEmpty(cliente.Endereco.Estado) ? string.Empty : cliente.Endereco.Estado);
                viewModel.Endereco = endereco.ToString();
            }

            return viewModel;
        }
    }

    public class EnderecoViewModel
    {
        public int? EnderecoId { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string CEP { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public static EnderecoViewModel MapearViewModel(EnderecoRetorno endereco)
        {
            if (endereco == null) return new EnderecoViewModel();

            var model = new EnderecoViewModel()
            {
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado
            };

            return model;
        }

        public EnderecoEnvio MapearModel()
        {
            return new EnderecoEnvio()
            {
                Rua = this.Rua,
                Numero = this.Numero,
                Complemento = this.Complemento,
                Bairro = this.Bairro,
                Cidade = this.Cidade,
                Estado = this.Estado
            };
        }
    }
}
