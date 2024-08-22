using Questao5.Domain.Entities;

namespace Questao5.Tests.TestesUnitarios
{
    public class MovimentoTests
    {
        [Fact]
        public void Deve_Criar_Movimento_Com_Dados_Validos()
        {
            var idContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var dataMovimento = "2024-08-22";
            var tipoMovimento = 'C';
            var valor = 100.0;

            var movimento = new Movimento(idContaCorrente, dataMovimento, tipoMovimento, valor);

            Assert.NotNull(movimento.IdMovimento);
            Assert.Equal(idContaCorrente, movimento.IdContaCorrente);
            Assert.Equal(dataMovimento, movimento.DataMovimento);
            Assert.Equal(tipoMovimento, movimento.TipoMovimento);
            Assert.Equal(valor, movimento.Valor);
        }

        [Fact]
        public void Deve_Manter_Valores_Quando_Propriedades_Sao_Atribuidas()
        {
            var movimento = new Movimento();
            var idMovimento = Guid.NewGuid().ToString().ToUpper();
            var idContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var dataMovimento = "2024-08-22";
            var tipoMovimento = 'D';
            var valor = 50.0;

            movimento.IdMovimento = idMovimento;
            movimento.IdContaCorrente = idContaCorrente;
            movimento.DataMovimento = dataMovimento;
            movimento.TipoMovimento = tipoMovimento;
            movimento.Valor = valor;

            Assert.Equal(idMovimento, movimento.IdMovimento);
            Assert.Equal(idContaCorrente, movimento.IdContaCorrente);
            Assert.Equal(dataMovimento, movimento.DataMovimento);
            Assert.Equal(tipoMovimento, movimento.TipoMovimento);
            Assert.Equal(valor, movimento.Valor);
        }
    }
}
