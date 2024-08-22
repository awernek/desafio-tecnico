using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Queries.Responses
{
    public class ObterMovimentoResponse : ResponseBase, IRequest<ObterMovimentoResponse>
    {
        public string IdMovimento { get; set; }
        public string NomeTitular { get; set; }
        public int NumeroConta { get; set; }
        public DateTime DataHoraResposta { get; set; }
        public double SaldoAtual { get; set; }
    }
}
