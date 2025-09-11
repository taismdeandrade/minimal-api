using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Infraestrutura.Db;
using MinimalAPI.Servicos;

namespace Test.Servicos
{
    [TestClass]
    public class VeiculoServicoTest
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
        public void TesteSalvarVeiculo()
        {
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Nome = "teste";
            veiculo.Marca = "test";
            veiculo.Ano = 2000;

            var veiculoServico = new VeiculoServico(context);

            //Act
            veiculoServico.Incluir(veiculo);

            //Asert
            Assert.AreEqual(1, veiculoServico.Todos(1).Count());
            Assert.AreEqual("teste", veiculo.Nome);
            Assert.AreEqual("test", veiculo.Marca);
            Assert.AreEqual(2000, veiculo.Ano);
        }

        [TestMethod]
        public void TesteBuscarVeiculo()
        {
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

            var veiculo = new Veiculo();
            veiculo.Nome = "teste";
            veiculo.Marca = "test";
            veiculo.Ano = 2000;

            var veiculoServico = new VeiculoServico(context);

            //Act
            veiculoServico.Incluir(veiculo);
            var veiculoBd = veiculoServico.BuscaPorId(veiculo.Id);

            //Asert
            Assert.AreEqual(1, veiculoBd.Id);

        }
    }
}