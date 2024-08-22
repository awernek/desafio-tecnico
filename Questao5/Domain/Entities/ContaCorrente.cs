namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public ContaCorrente() { }

        public ContaCorrente(int numero, string nome, bool ativo)
        {
            IdContaCorrente = Guid.NewGuid().ToString().ToUpper();
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
        }

        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
