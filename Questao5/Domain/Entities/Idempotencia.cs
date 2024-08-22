﻿namespace Questao5.Domain.Entities
{
    public class Idempotencia
    {
        public Idempotencia() { }

        public Idempotencia(string requisicao, string resultado)
        {
            ChaveIdempotencia = Guid.NewGuid().ToString().ToUpper();
            Requisicao = requisicao;
            Resultado = resultado;
        }

        public string ChaveIdempotencia { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }
    }
}
