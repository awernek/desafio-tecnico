using System;

namespace Questao1
{
    class ContaBancaria
    {
        public int NumeroConta { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numeroConta, string titular, double depositoInicial)
        {
            Saldo = depositoInicial;
            NumeroConta = numeroConta;
            Titular = titular;
        }

        public ContaBancaria(int numeroConta, string titular)
        {
            NumeroConta = numeroConta;
            Titular = titular;
        }

        public void Deposito(double valor)
        {
            ValidarValorPositivo(valor);

            Saldo += valor;
        }

        public void Saque(double valor)
        {
            ValidarValorPositivo(valor);

            if (valor > Saldo)
                throw new InvalidOperationException("O saldo é insuficiente");

            Saldo -= valor;
        }

        private static void ValidarValorPositivo(double valor)
        {
            if (valor <= 0)
                throw new ArgumentException($"O valor deve ser positivo: {valor}.");
        }

        public override string ToString()
        {
            return $"Conta: {NumeroConta}, Titular: {Titular}, Saldo: {Saldo}";
        }
    }
}
