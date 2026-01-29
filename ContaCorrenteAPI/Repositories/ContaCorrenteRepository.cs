using ContaCorrenteAPI.Domain.Entities;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using ContaCorrenteAPI.Domain.Models;

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

        public async Task<ContaCorrente> ObterContaCorrentePeloNumeroENomeAsync(long numero, string nome)
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

        public async Task<ContaCorrente> ObterContaCorrentePeloIdAsync(string id)
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

        public async Task<ResultadoBase> SalvarRegistroAsync(ContaCorrente contaCorrente)
        {
            ResultadoBase res;
            try
            {
                res = IsValidContaCorrente(contaCorrente);

                if (!res.Successo)
                {
                    return res;
                }

                res = await ExistsContaCorrenteByNumeroAsync(contaCorrente.Numero);

                if (res.Successo)
                {
                    var cc = await ObterContaCorrentePeloNumeroAsync(contaCorrente.Numero);
                    contaCorrente.IdContaCorrente = string.IsNullOrWhiteSpace(cc?.IdContaCorrente) ? string.Empty : cc.IdContaCorrente;
                    return await UpdateAsync(contaCorrente);
                }

                return await InsertAsync(contaCorrente);
            }
            catch
            {
               res = new ResultadoBase()  { Successo = false };
               res.MensagensDeErro.Add("Ocorreu um erro ao salvar registro da conta corrente.");
                return res;
            }
        }


        public async Task<ContaCorrente?> ObterContaCorrentePeloNumeroAsync(long numeroContaCorrente)
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



        public async Task<ResultadoBase> ExistsContaCorrenteByNumeroAsync(long numeroContaCorrente)
        {
            ResultadoBase res;
            try
            {

                if (numeroContaCorrente <= 0)
                {
                    res = new ResultadoBase() { Successo = false };
                    res.MensagensDeErro.Add("Numero da conta corrente informado está inválido.");
                    return res;
                }

                string sqlExists = @"SELECT CASE WHEN EXISTS (SELECT 1 FROM [BancoContaCorrente].[dbo].[contacorrente] WHERE numero = @numero) 
                                    THEN 1 
                                    ELSE 0 
                                    END AS ExisteRegistro;";


                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                {
                    bool exist = conn.QuerySingle<bool>(sqlExists, new { numero = numeroContaCorrente });

                    if (exist)
                    {
                        res = new ResultadoBase() { Successo = true };
                    }
                    else
                    {
                        res = new ResultadoBase() { Successo = false };
                        res.MensagensDeErroValidacao.Add("Não existe conta corrente cadastrada para esse número da conta informado.");
                    }
                }

                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao verificar se existe conta corrente cadastrada.");
                return res;
            }
        }


        private async Task<ResultadoBase> InsertAsync(ContaCorrente contaCorrente)
        {
            ResultadoBase res;
            try
            {

                res = IsValidContaCorrente(contaCorrente);

                if(!res.Successo)
                {
                    return res;
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

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                {
                    int insertAffectedRows = await conn.ExecuteAsync(sqlInsert, contaCorrente);

                    if (insertAffectedRows > 0)
                    {
                        res = new ResultadoBase() { Successo = true };
                    }
                    else
                    {
                        res = new ResultadoBase() { Successo = false };
                        res.MensagensDeErro.Add("Não foi realizado o insert da conta corrente.");
                    }                    
                }

                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao inserir conta corrente.");
                return res;
            }
        }

        private async Task<ResultadoBase> UpdateAsync(ContaCorrente contaCorrente)
        {
            ResultadoBase res;
            try
            {
                res = IsValidContaCorrente(contaCorrente);

                if (!res.Successo)
                {
                    return res;
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

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                {
                    int updateAffectedRows = await conn.ExecuteAsync(sqlUpdate, contaCorrente);

                    if (updateAffectedRows > 0)
                    {
                        res = new ResultadoBase() { Successo = true };
                    }
                    else
                    {
                        res = new ResultadoBase() { Successo = false };
                        res.MensagensDeErro.Add("Não foi realizado o update da conta corrente.");
                    }
                }

                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao atualizar conta corrente.");
                return res;
            }
        }

        private ResultadoBase IsValidContaCorrente(ContaCorrente contaCorrente)
        {
            ResultadoBase res;

            if (contaCorrente == null)
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("Paramentro conta corrente nulo.");
                return res;
            }

            if (contaCorrente.Numero <= 0)
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("Numero da conta corrente inválido.");
                return res;
            }

            res = new ResultadoBase() { Successo = true };
            return res;
        }

        public async Task<ResultadoBase> InativarContaCorrentePeloNumeroAsync(long numeroContaCorrente, string senha)
        {
            ResultadoBase res;
            try
            {
                bool senhaValida = await _userRepository.SenhaValida(numeroContaCorrente, senha);

                if (!senhaValida)
                {

                    res = new ResultadoBase() { Successo = false };
                    res.MensagensDeErro.Add("INVALID_DOCUMENT");
                    return res;
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

                if (inativarAffectedRows > 0)
                {
                    res = new ResultadoBase() { Successo = true };
                    return res;
                }

                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao inativar conta corrente pelo número da conta. Não houve registros afetados.");
                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao inativar conta corrente pelo número da conta.");
                return res;
            }
        }
    }
}