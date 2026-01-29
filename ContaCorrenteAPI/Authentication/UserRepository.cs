namespace ContaCorrenteAPI.Authentication
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Data;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task<User> GetUserForAuthentication(long numeroContaCorrente)
        {
            const string sql = @"SELECT
                                   [numero] as NumeroContaCorrente
                                  , [nome]
                                  , 'admin' as perfil
                                  , [senha]
                              FROM [BancoContaCorrente].[dbo].[contacorrente]
                              WHERE numero = @numero
                              and ativo = 1;";

            User u;
            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                u = await conn.QuerySingleOrDefaultAsync<User>(sql, new { numero = numeroContaCorrente });
            }

            return u;
        }         

        public async Task<bool> SenhaValida(long numeroContaCorrente, string senhaInput)
        {
            var user = await GetUserForAuthentication(numeroContaCorrente);
            if (user == null)
            {
                return false;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(senhaInput);
            return BCrypt.Net.BCrypt.Verify(user.Senha, passwordHash);
        }
    }
}