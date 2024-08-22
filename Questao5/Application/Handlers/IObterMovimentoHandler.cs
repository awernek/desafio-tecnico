using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Handlers
{
    public interface IObterMovimentoHandler
    {
        ObterMovimentoResponse Handle(ObterMovimentoRequest command);
    }
}
