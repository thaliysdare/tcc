using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Inserir(Usuario model)
        {
            var modelNovo = default(Usuario);
            using (var scope = new TransactionScope())
            {
                model.IdcStatusUsuario = Enums.StatusUsuarioEnum.Ativo;
                modelNovo = _usuarioRepository.InserirERecuperar(model);
                scope.Complete();
            }
            return modelNovo;
        }

        public void Editar(int id, Usuario model)
        {
            var originalModel = _usuarioRepository.RecuperarPorId(id);
            VerificarLiberadoParaAlteracao(originalModel);

            using (var scope = new TransactionScope())
            {
                originalModel.Senha = model.Senha;
                originalModel.Email = model.Email;
                originalModel.Nome = model.Nome;
                originalModel.Sobrenome = model.Sobrenome;

                _usuarioRepository.Editar(originalModel);
                scope.Complete();
            }
        }

        public void Inativar(int id)
        {
            using (var scope = new TransactionScope())
            {
                var originalModel = _usuarioRepository.RecuperarPorId(id);
                originalModel.IdcStatusUsuario = Enums.StatusUsuarioEnum.Inativo;
                _usuarioRepository.Editar(originalModel);

                scope.Complete();
            }
        }

        #region[Metodos auxiliares]
        private void VerificarLiberadoParaAlteracao(Usuario model)
        {
            if (model == null)
                throw new System.Exception("Usuario não encontrado");
            if (model.IdcStatusUsuario == Enums.StatusUsuarioEnum.Inativo)
                throw new System.Exception("Usuario inativo não pode ser alterado");
        }
        #endregion
    }
}
