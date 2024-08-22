using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories;
using System;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly IDbConnection _dbConnection;

        public MovimentoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Movimento>> ObterMovimentosPorContaCorrente(string idContaCorrente)
        {
            var listaMovimentos = new List<Movimento>();
            var sql = @"
                            SELECT idmovimento     AS IdMovimento,
                                   idcontacorrente AS IdContaCorrente,
                                   datamovimento   AS DataMovimento,
                                   tipomovimento   AS TipoMovimento,
                                   valor           AS Valor
                            FROM   movimento
                            WHERE  idcontacorrente = @IdContaCorrente";

            var results = await _dbConnection.QueryAsync<Movimento>(sql, new { IdContaCorrente = idContaCorrente.ToString() });

            if (results != null)
            {
                foreach (var result in results)
                {
                    var movimento = new Movimento();

                    movimento.IdMovimento = result.IdMovimento.ToString();
                    movimento.IdContaCorrente = result.IdContaCorrente.ToString();
                    movimento.DataMovimento = result.DataMovimento.ToString();
                    movimento.TipoMovimento = (char)result.TipoMovimento;
                    movimento.Valor = double.Parse(result.Valor.ToString());

                    listaMovimentos.Add(movimento);
                }
            }

            return listaMovimentos;

        }

        public async Task Salvar(Movimento movimento)
        {
            var query = @"
            INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
            VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

            var parametros = new
            {
                movimento.IdMovimento,
                movimento.IdContaCorrente,
                movimento.DataMovimento,
                TipoMovimento = ((char)movimento.TipoMovimento).ToString(),
                movimento.Valor
            };

            await _dbConnection.ExecuteAsync(query, parametros);
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
