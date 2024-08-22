using Questao5.Domain.Entities;

namespace Questao5.Tests.TestesUnitarios
{
    public class ContaCorrenteTests
    {
        [Fact]
        public void Deve_Criar_ContaCorrente_Com_Dados_Validos()
        {
            var numero = 123456;
            var nome = "João Silva";
            var ativo = true;

            var contaCorrente = new ContaCorrente(numero, nome, ativo);

            Assert.NotNull(contaCorrente.IdContaCorrente);
            Assert.Equal(numero, contaCorrente.Numero);
            Assert.Equal(nome, contaCorrente.Nome);
            Assert.Equal(ativo, contaCorrente.Ativo);
        }

        [Fact]
        public void Deve_Manter_Valores_Quando_Propriedades_Sao_Atribuidas()
        {
            var contaCorrente = new ContaCorrente();
            var idContaCorrente = Guid.NewGuid().ToString().ToUpper();
            var numero = 654321;
            var nome = "Maria Oliveira";
            var ativo = false;

            contaCorrente.IdContaCorrente = idContaCorrente;
            contaCorrente.Numero = numero;
            contaCorrente.Nome = nome;
            contaCorrente.Ativo = ativo;

            Assert.Equal(idContaCorrente, contaCorrente.IdContaCorrente);
            Assert.Equal(numero, contaCorrente.Numero);
            Assert.Equal(nome, contaCorrente.Nome);
            Assert.Equal(ativo, contaCorrente.Ativo);
        }
    }
}
