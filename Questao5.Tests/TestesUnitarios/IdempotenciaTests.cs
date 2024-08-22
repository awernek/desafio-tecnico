using Questao5.Domain.Entities;

namespace Questao5.Tests.TestesUnitarios
{
    public class IdempotenciaTests
    {
        [Fact]
        public void Deve_Criar_Idempotencia_Com_Dados_Validos()
        {
            var requisicao = "{\"Id\":\"12345\"}";
            var resultado = "Success";

            var idempotencia = new Idempotencia(requisicao, resultado);

            Assert.NotNull(idempotencia.ChaveIdempotencia);
            Assert.Equal(requisicao, idempotencia.Requisicao);
            Assert.Equal(resultado, idempotencia.Resultado);
        }

        [Fact]
        public void Deve_Manter_Valores_Quando_Propriedades_Sao_Atribuidas()
        {
            var idempotencia = new Idempotencia();
            var chaveIdempotencia = Guid.NewGuid().ToString().ToUpper();
            var requisicao = "{\"Id\":\"67890\"}";
            var resultado = "Failed";

            idempotencia.ChaveIdempotencia = chaveIdempotencia;
            idempotencia.Requisicao = requisicao;
            idempotencia.Resultado = resultado;

            Assert.Equal(chaveIdempotencia, idempotencia.ChaveIdempotencia);
            Assert.Equal(requisicao, idempotencia.Requisicao);
            Assert.Equal(resultado, idempotencia.Resultado);
        }
    }
}
