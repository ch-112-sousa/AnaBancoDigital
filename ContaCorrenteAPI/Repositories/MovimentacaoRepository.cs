using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Models;
using System.Data;

namespace ContaCorrenteAPI.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly IDbConnection _dbConnection;
        
        public MovimentacaoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ResultadoBase> RegistrarMovimentacaoAsync(MovimentoConta movimento)
        {
            throw new NotImplementedException();
        }
    } 
}