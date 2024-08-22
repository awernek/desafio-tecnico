using Dapper;
using FluentAssertions.Equivalency;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContaCorrenteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ContaCorrente> ObterPorId(string idContaCorrente)
        {
            var sql = @"SELECT 
                                    idcontacorrente AS IdContaCorrente,
                                    numero AS Numero,
                                    nome AS Nome,
                                    ativo AS Ativo
                                FROM contacorrente WHERE idcontacorrente = @IdContaCorrente";

            return await _dbConnection.QuerySingleOrDefaultAsync<ContaCorrente>(sql, new { IdContaCorrente = idContaCorrente.ToString() });
        }

        public async Task<ContaCorrente> ObterPorNumero(int numero)
        {
            var query = @"
                        SELECT 
                            idcontacorrente AS IdContaCorrente,
                            numero AS Numero,
                            nome AS Nome,
                            ativo AS Ativo
                        FROM 
                            contacorrente
                        WHERE 
                            numero = @Numero";

            var parameters = new
            {
                Numero = numero,
            };

            var result = await _dbConnection.QuerySingleOrDefaultAsync<ContaCorrente>(query, parameters);

            return result;
        }
    }
}
