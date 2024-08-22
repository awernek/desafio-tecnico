using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers
{
    public class ObterMovimentoHandler : IRequestHandler<ObterMovimentoRequest, ObterMovimentoResponse>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public ObterMovimentoHandler(
            IMovimentoRepository movimentoRepository,
            IContaCorrenteRepository contaCorrenteRepository,
            IIdempotenciaRepository idempotenciaRepository)
        {
            _movimentoRepository = movimentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<ObterMovimentoResponse> Handle(ObterMovimentoRequest command, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository.ObterPorId(command.IdContaCorrente);

            if (conta == null)
            {
                return new ObterMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Conta não encontrada.",
                    TipoErro = "INVALID_ACCOUNT"
                };
            }

            if (!conta.Ativo)
            {
                return new ObterMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Conta inativa.",
                    TipoErro = "INACTIVE_ACCOUNT"
                };
            }

            var movimentos = await _movimentoRepository.ObterMovimentosPorContaCorrente(command.IdContaCorrente);

            return new ObterMovimentoResponse
            {
                Sucesso = true,
                NumeroConta = conta.Numero,
                NomeTitular = conta.Nome,
                SaldoAtual = CalcularSaldoDosMovimentos(movimentos),
                DataHoraResposta = DateTime.Now
            };
        }

        private static double CalcularSaldoDosMovimentos(IEnumerable<Movimento> movimentos)
        {
            return movimentos
                            .Where(m => (TipoMovimento)m.TipoMovimento == TipoMovimento.Credito)
                            .Sum(m => m.Valor) -
                            movimentos
                            .Where(m => (TipoMovimento)m.TipoMovimento == TipoMovimento.Debito)
                            .Sum(m => m.Valor);
        }
    }
}
