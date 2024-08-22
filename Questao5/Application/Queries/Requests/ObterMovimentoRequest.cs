using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ObterMovimentoRequest : IRequest<ObterMovimentoResponse>
    {
        public string IdContaCorrente { get; set; }
        //public  string ChaveIdempotencia { get; set; }
    }
}
