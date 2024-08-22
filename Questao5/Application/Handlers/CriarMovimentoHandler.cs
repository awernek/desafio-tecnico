using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Handlers
{
    public class CriarMovimentoHandler : IRequestHandler<CriarMovimentoRequest, CriarMovimentoResponse>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IIdempotenciaRepository _idempotenciaRepository;

        public CriarMovimentoHandler(
            IMovimentoRepository movimentoRepository,
            IContaCorrenteRepository contaCorrenteRepository,
            IIdempotenciaRepository idempotenciaRepository)
        {
            _movimentoRepository = movimentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
            _idempotenciaRepository = idempotenciaRepository;
        }

        public async Task<CriarMovimentoResponse> Handle(CriarMovimentoRequest command, CancellationToken cancellationToken)
        {
            var conta = await _contaCorrenteRepository
                .ObterPorId(command.IdContaCorrente);

            var tipoMovimento = ObterTipoMovimento(command.TipoMovimento);

            if (conta == null)
            {
                return new CriarMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Conta não encontrada.",
                    TipoErro = "INVALID_ACCOUNT"
                };
            }

            if (!conta.Ativo)
            {
                return new CriarMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Conta inativa.",
                    TipoErro = "INACTIVE_ACCOUNT"
                };
            }

            if (command.Valor <= 0)
            {
                return new CriarMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Valor inválido.",
                    TipoErro = "INVALID_VALUE"
                };
            }

            if (TipoMovimento.Debito.Equals(tipoMovimento)
                && !await ValidarSaldoPositivo(command))
            {
                return new CriarMovimentoResponse
                {
                    Sucesso = false,
                    Mensagem = "Valor maior que saldo disponível.",
                    TipoErro = "INVALID_VALUE"
                };
            }

            var movimento = new Movimento(command.IdContaCorrente, command.DataMovimento, Convert.ToChar(command.TipoMovimento), command.Valor);

            await _movimentoRepository.Salvar(movimento);

            var idempotencia = new Idempotencia
            {
                ChaveIdempotencia = Guid.NewGuid().ToString(),
                Requisicao = JsonConvert.SerializeObject(command),
                Resultado = movimento.IdMovimento.ToString()
            };
            await _idempotenciaRepository.SalvarIdempotencia(idempotencia);

            return new CriarMovimentoResponse
            {
                Sucesso = true,
                IdMovimento = movimento.IdMovimento,
                IdContaCorrente = movimento.IdContaCorrente,
                DataMovimento = movimento.DataMovimento,
                TipoMovimento = TipoMovimentoToString(movimento.TipoMovimento),
                Valor = movimento.Valor
            };
        }

        private async Task<bool> ValidarSaldoPositivo(CriarMovimentoRequest command)
        {
            var movimentos = await _movimentoRepository.ObterMovimentosPorContaCorrente(command.IdContaCorrente);
            double saldo = CalcularSaldoDosMovimentos(movimentos);

            return saldo > command.Valor;
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

        private string TipoMovimentoToString(char tipoMovimento)
        {
            switch (tipoMovimento)
            {
                case 'C':
                    return TipoMovimento.Credito.ToString();
                case 'D':
                    return TipoMovimento.Debito.ToString();
                default:
                    throw new ArgumentException("Valor inválido para TipoMovimento", nameof(tipoMovimento));
            }

        }

        public TipoMovimento ObterTipoMovimento(string tipo)
        {
            return tipo switch
            {
                "C" => TipoMovimento.Credito,
                "D" => TipoMovimento.Debito,
                _ => throw new ArgumentException("Tipo de movimento inválido")
            };
        }
    }
}
