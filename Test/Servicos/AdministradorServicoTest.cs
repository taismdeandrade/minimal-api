using Microsoft.EntityFrameworkCore;
using MinimalAPI.Infraestrutura.Db;
using Microsoft.Extensions.Configuration;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Servicos;
using System.Reflection;

namespace Test.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

            var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestandoSalvarAdministrador()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            var administradorServico = new AdministradorServico(context);

            //Act
            administradorServico.Incluir(adm);

            //Asert
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
            Assert.AreEqual("teste@teste.com", adm.Email);
            Assert.AreEqual("teste", adm.Senha);
            Assert.AreEqual("Adm", adm.Perfil);
        }


        [TestMethod]
        public void TestandoBuscaAdministrador()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            var administradorServico = new AdministradorServico(context);

            //Act
            administradorServico.Incluir(adm);
            var admDoBanco = administradorServico.BuscaPorId(adm.Id);

            //Asert
            Assert.AreEqual(1, admDoBanco.Id);
        }
    }
}