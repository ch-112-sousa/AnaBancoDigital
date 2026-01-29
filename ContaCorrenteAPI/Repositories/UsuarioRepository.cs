using ContaCorrenteAPI.Authentication;

namespace ContaCorrenteAPI.Repositories
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Data;
    using System.Threading.Tasks;

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task<UsuarioModel> ObterUsuarioParaAutenticacao(long numeroContaCorrente)
        {
            const string sql = @"SELECT
                                   [numero] as NumeroContaCorrente
                                  , [nome]
                                  , 'admin' as perfil
                                  , [senha]
                              FROM [BancoContaCorrente].[dbo].[contacorrente]
                              WHERE numero = @numero
                              and ativo = 1;";

            UsuarioModel u;
            using (var conn = new SqlConnection(_connection.ConnectionString))
            {
                u = await conn.QuerySingleOrDefaultAsync<UsuarioModel>(sql, new { numero = numeroContaCorrente });
            }

            return u;
        }         

        public async Task<bool> SenhaValida(long numeroContaCorrente, string senhaInput)
        {
            var user = await ObterUsuarioParaAutenticacao(numeroContaCorrente);
            if (user == null)
            {
                return false;
            }

            var passwordHash = user.Senha;
            return BCrypt.Net.BCrypt.Verify(senhaInput, passwordHash);
        }

        public string HashPassword(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
    }
}