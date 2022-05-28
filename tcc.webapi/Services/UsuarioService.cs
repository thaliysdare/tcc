using System.Linq;
using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioFuncionalidadeRepository _usuarioFuncionalidadeRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUsuarioFuncionalidadeRepository usuarioFuncionalidadeRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioFuncionalidadeRepository = usuarioFuncionalidadeRepository;
        }

        public Usuario Inserir(Usuario model)
        {
            if (string.IsNullOrEmpty(model.Senha))
                throw new System.Exception("Usuário precisa ter uma senha");

            var modelNovo = default(Usuario);
            using (var scope = new TransactionScope())
            {
                model.IdcStatusUsuario = Enums.StatusUsuarioEnum.Ativo;
                modelNovo = _usuarioRepository.InserirERecuperar(model);

                foreach (var item in model.UsuarioFuncionalidade)
                {
                    item.UsuarioId = modelNovo.UsuarioId;
                    _usuarioFuncionalidadeRepository.Inserir(item);
                }

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
                if (!string.IsNullOrEmpty(model.Senha))
                    originalModel.Senha = model.Senha;

                originalModel.Email = model.Email;
                originalModel.Nome = model.Nome;
                originalModel.Sobrenome = model.Sobrenome;
                _usuarioRepository.Editar(originalModel);

                if (model.UsuarioFuncionalidade != null && model.UsuarioFuncionalidade.Count > 0)
                {
                    var listaFuncionalidadeIds = model.UsuarioFuncionalidade.Select(x => x.FuncionalidadeId).ToList();

                    var listaFuncionalidadesAExcluir = originalModel.UsuarioFuncionalidade.Where(x => !listaFuncionalidadeIds.Contains(x.FuncionalidadeId)).ToList();
                    var listaFuncionalidadesAIncluir = listaFuncionalidadeIds.Where(x => !originalModel.UsuarioFuncionalidade.Any(y => y.FuncionalidadeId == x)).ToList();

                    foreach (var item in listaFuncionalidadesAExcluir)
                        _usuarioFuncionalidadeRepository.Excluir(item);

                    foreach (var item in listaFuncionalidadesAIncluir)
                    {
                        _usuarioFuncionalidadeRepository.Inserir(new UsuarioFuncionalidade()
                        {
                            FuncionalidadeId = item,
                            UsuarioId = originalModel.UsuarioId
                        });
                    }
                }
                
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
