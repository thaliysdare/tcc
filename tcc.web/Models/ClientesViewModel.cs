using System;
using System.Collections.Generic;
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
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPFOuCNPJ { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public int? EnderecoId { get; set; }

        public static ClienteViewModel MapearViewModel(ClienteRetorno cliente)
        {
            return new ClienteViewModel()
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Sobrenome = cliente.Sobrenome,
                CPFOuCNPJ = cliente.CPFOuCNPJ,
                Telefone1 = cliente.Telefone1,
                Telefone2 = cliente.Telefone2,
                EnderecoId = cliente.EnderecoId
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
}
