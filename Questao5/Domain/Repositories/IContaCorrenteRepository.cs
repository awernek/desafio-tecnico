using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> ObterPorId(string idContaCorrente);
        Task<ContaCorrente> ObterPorNumero(int numero);
    }
}
