using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tcc.webapi.Models;

namespace tcc.webapi.Services.IServices
{
    public interface IClienteService : IGenericoService
    {
        Cliente Inserir(Cliente cliente);
        void Editar(Cliente cliente);
        void Excluir(Cliente cliente);
    }
}
