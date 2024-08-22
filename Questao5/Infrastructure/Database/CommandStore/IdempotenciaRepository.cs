using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;
using System.Data;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly IDbConnection _dbConnection;

        public IdempotenciaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Idempotencia> ObterPorChave(string chaveIdempotencia)
        {
            var query = @"
                        SELECT 
                            chave_idempotencia AS ChaveIdempotencia, 
                            requisicao AS Requisicao, 
                            resultado AS Resultado
                        FROM idempotencia 
                        WHERE chave_idempotencia = @ChaveIdempotencia";

            var parameters = new
            {
                ChaveIdempotencia = chaveIdempotencia,
            };

            var result = await _dbConnection.QueryFirstOrDefaultAsync<Idempotencia>(query, parameters);

            return result;
        }

        public async Task SalvarIdempotencia(Idempotencia idempotencia)
        {
            var query = @"
            INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado)
                          VALUES (@ChaveIdempotencia, @Requisicao, @Resultado);";

            await _dbConnection.ExecuteAsync(query, idempotencia);
        }
    }
}
