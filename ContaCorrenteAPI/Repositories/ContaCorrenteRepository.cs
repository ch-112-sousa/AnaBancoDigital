using ContaCorrenteAPI.Domain.Entities;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using ContaCorrenteAPI.Authentication;

namespace ContaCorrenteAPI.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IUsuarioRepository _userRepository;

        public ContaCorrenteRepository(IDbConnection dbConnection, IUsuarioRepository userRepository)  
        {
            _dbConnection = dbConnection;
            _userRepository = userRepository;
        }

        public async Task<ContaCorrente> GetContaCorrenteByNumeroENomeAsync(long numero, string nome)
        {
            ContaCorrente cc;
            string sqlGetById = @"SELECT
                           [idcontacorrente]
                          ,[numero]
                          ,[nome]
                          ,[ativo]
                          ,[senha]
                          ,[salt]
                      FROM [BancoContaCorrente].[dbo].[contacorrente]
                      WHERE numero = @numeroContaCorrente
                            and nome = @nome";

            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                cc = await conn.QueryFirstAsync<ContaCorrente>(sqlGetById, new { numeroContaCorrente = numero, nome = nome });
            }

            return cc;
        }

        public async Task<ContaCorrente> GetContaCorrenteByIdAsync(string id)
        {
            ContaCorrente cc;
            string sqlGetById = @"SELECT
                           [idcontacorrente]
                          ,[numero]
                          ,[nome]
                          ,[ativo]
                          ,[senha]
                          ,[salt]
                      FROM [BancoContaCorrente].[dbo].[contacorrente]
                      WHERE idcontacorrente = @idContaCorrente;";

            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                cc = await conn.QueryFirstAsync<ContaCorrente>(sqlGetById, new { idContaCorrente = id });
            }

            return cc;
        }

        public async Task<bool> SaveAsync(ContaCorrente contaCorrente)
        {
            if (contaCorrente == null || contaCorrente.Numero <= 0)
            {
                return false;
            }

            bool existe;

            existe = await ExistsContaCorrenteByNumeroAsync(contaCorrente.Numero);

            if (existe)
            {
                var cc = await GetContaCorrenteByNumeroAsync(contaCorrente.Numero);
                contaCorrente.IdContaCorrente = string.IsNullOrWhiteSpace(cc?.IdContaCorrente) ? string.Empty : cc.IdContaCorrente;
                return await UpdateAsync(contaCorrente);
            }

            return await InsertAsync(contaCorrente);
        }


        public async Task<ContaCorrente?> GetContaCorrenteByNumeroAsync(long numeroContaCorrente)
        {
            if (numeroContaCorrente <= 0)
            {
                return null;
            }

            string sqlByNumero = @"SELECT top 1
                           [idcontacorrente]
                          ,[numero]
                          ,[nome]
                          ,[ativo]
                          ,[senha]
                          ,[salt]
                      FROM [BancoContaCorrente].[dbo].[contacorrente]
                      WHERE numero = @numero;";

            ContaCorrente cc;
            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                cc = await conn.QueryFirstAsync<ContaCorrente>(sqlByNumero, new { numero = numeroContaCorrente });
            }

            return cc;
        }



        public async Task<bool> ExistsContaCorrenteByNumeroAsync(long numeroContaCorrente)
        {
            if (numeroContaCorrente <= 0)
            {
                return false;
            }

            string sqlExists = @"SELECT CASE WHEN EXISTS (SELECT 1 FROM [BancoContaCorrente].[dbo].[contacorrente] WHERE numero = @numero) 
                                    THEN 1 
                                    ELSE 0 
                                    END AS ExisteRegistro;";

            bool exist;
            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                exist = conn.QuerySingle<bool>(sqlExists, new { numero = numeroContaCorrente });
            }

            return exist;
        }


        private async Task<bool> InsertAsync(ContaCorrente contaCorrente)
        {
            if (!IsValidContaCorrenteInsert(contaCorrente))
            {
                return false;
            }

            contaCorrente.IdContaCorrente = Guid.NewGuid().ToString();
            contaCorrente.Senha = _userRepository.HashPassword(contaCorrente.Senha);
            string sqlInsert = @"INSERT INTO [dbo].[contacorrente]
                                (
                                         [idcontacorrente]
                                        ,[numero]
                                        ,[nome]
                                        ,[ativo]
                                        ,[senha]
                                        ,[salt]
                                 )
                                 VALUES (
                                        @idcontacorrente
                                       ,@numero
                                       ,@nome
                                       ,@ativo
                                       ,@senha
                                       ,@salt
                                    );";

            int insertAffectedRows;
            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                insertAffectedRows = await conn.ExecuteAsync(sqlInsert, contaCorrente);
            }

            return insertAffectedRows > 0;
        }

        private async Task<bool> UpdateAsync(ContaCorrente contaCorrente)
        {
            if (!IsValidContaCorrenteUpdate(contaCorrente))
            {
                return false;
            }

            contaCorrente.Senha = _userRepository.HashPassword(contaCorrente.Senha);

            string sqlUpdate = @"UPDATE [dbo].[contacorrente]
                                     SET   [idcontacorrente] = @idcontacorrente
                                          ,[numero] = @numero
                                          ,[nome] = @nome
                                          ,[ativo] = @ativo
                                          ,[senha] = @senha
                                          ,[salt] = @salt
                                     WHERE idcontacorrente = @idcontacorrente;";

            int updateAffectedRows = 0;
            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                updateAffectedRows = await conn.ExecuteAsync(sqlUpdate, contaCorrente);
            }

            return updateAffectedRows > 0;
        }


        private bool IsValidContaCorrenteUpdate(ContaCorrente contaCorrente)
        {
            if (contaCorrente == null)
            {
                return false;
            }

            bool idEmpty = string.IsNullOrWhiteSpace(contaCorrente.IdContaCorrente);
            bool numeroInvalido = contaCorrente.Numero <= 0;

            if (idEmpty || numeroInvalido)
            {
                return false;
            }

            return true;
        }


        private bool IsValidContaCorrenteInsert(ContaCorrente contaCorrente)
        {
            if (contaCorrente == null)
            {
                return false;
            }

            bool idEmpty = string.IsNullOrWhiteSpace(contaCorrente.IdContaCorrente);
            bool numeroInvalido = contaCorrente.Numero <= 0;

            if (!idEmpty || numeroInvalido)
            {
                return false;
            }

            return true;
        }

        public async Task<string> InativarContaCorrentePeloNumeroAsync(long numeroContaCorrente, string senha)
        {
            bool senhaValida = await _userRepository.SenhaValida(numeroContaCorrente, senha);

            if (!senhaValida)
            {
                return "senha inválida";
            }

            string sqlInativar = @"UPDATE [dbo].[contacorrente]
                                     SET   
                                          [ativo] = 0                                          
                                     WHERE numero = @numero;";


            int inativarAffectedRows = 0;
            using (var conn = new SqlConnection(_dbConnection.ConnectionString))
            {
                inativarAffectedRows = await conn.ExecuteAsync(sqlInativar, new { numero = numeroContaCorrente });
            }

            if(inativarAffectedRows > 0)
            {
                return string.Empty;
            }

            return "Erro ao inativar registro conta corrente: " + numeroContaCorrente;
        } 
    }
}