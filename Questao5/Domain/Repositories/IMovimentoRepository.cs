using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task Salvar(Movimento movimento);
        Task<IEnumerable<Movimento>> ObterMovimentosPorContaCorrente(string idContaCorrente);
    }
}
