using MinimalAPI.Dominio.Entidades;

namespace Test.Domain
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var veiculo = new Veiculo();

            veiculo.Id = 1;
            veiculo.Nome = "teste";
            veiculo.Marca = "test";
            veiculo.Ano = 2000;

            Assert.AreEqual(1, veiculo.Id);
            Assert.AreEqual("teste", veiculo.Nome);
            Assert.AreEqual("test", veiculo.Marca);
            Assert.AreEqual(2000, veiculo.Ano);
        }
    }
}