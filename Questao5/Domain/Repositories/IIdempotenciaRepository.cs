using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories
{
    public interface IIdempotenciaRepository
    {
        Task<Idempotencia> ObterPorChave(string chaveIdempotencia);
        Task SalvarIdempotencia(Idempotencia idempotencia);
    }
}
