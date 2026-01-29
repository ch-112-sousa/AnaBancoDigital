namespace ContaCorrenteAPI.Authentication
{
    using Dapper;
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
                                  ,[nome]
                                  ,'admin' as perfil
                              FROM [BancoContaCorrente].[dbo].[contacorrente]
                              WHERE numero = @numero
                              and ativo = 1;";
                                 
            var user = await _connection.QuerySingleOrDefaultAsync<User>(sql, new { numero = numeroContaCorrente });

            return user;
        }
    }
}