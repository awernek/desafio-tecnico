using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public Movimento() { }

        public Movimento(string idContaCorrente, string dataMovimento, char tipoMovimento, double valor)
        {
            IdMovimento = Guid.NewGuid().ToString().ToUpper();
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public char TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
