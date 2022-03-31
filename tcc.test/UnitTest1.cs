using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories;

namespace tcc.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var retorno = new OrdemServicoRepository(new BancoContexto(new DbContextOptions<BancoContexto>())).RecuperarPorId(1);
        }
    }
}
