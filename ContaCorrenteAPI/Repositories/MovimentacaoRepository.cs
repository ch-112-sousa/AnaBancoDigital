using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

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
            ResultadoBase res;
            try
            {
                res = IsValidMovimento(movimento);

                if (!res.Successo)
                {
                    return res;
                }
                
                string sqlInsert = @"INSERT INTO [dbo].[movimento]
                                    ([idmovimento]
                                    ,[idcontacorrente]
                                    ,[datamovimento]
                                    ,[tipomovimento]
                                    ,[valor])
                                VALUES
                                    (@idmovimento
                                    ,@idcontacorrente
                                    ,@datamovimento
                                    ,@tipomovimento
                                    ,@valor)";

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                {
                    int insertAffectedRows = await conn.ExecuteAsync(sqlInsert, movimento);

                    if (insertAffectedRows > 0)
                    {
                        res = new ResultadoBase() { Successo = true };
                    }
                    else
                    {
                        res = new ResultadoBase() { Successo = false };
                        res.MensagensDeErro.Add("Não foi realizado o insert da movimentacao.");
                    }
                }

                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao inserir movimentacao.");
                return res;
            }
        }

        public async Task<ResultadoBase> AtualizarMovimentacaoAsync(MovimentoConta movimento)
        {
            ResultadoBase res;
            try
            {
                res = IsValidMovimento(movimento);

                if (!res.Successo)
                {
                    return res;
                }

                string sqlInsert = @"UPDATE [dbo].[movimento]
                                           SET 
                                              ,[idcontacorrente] = @idcontacorrente
                                              ,[datamovimento] = @datamovimento
                                              ,[tipomovimento] = @tipomovimento
                                              ,[valor] = @valor
                                         WHERE idmovimento = @idmovimento;";

                using (var conn = new SqlConnection(_dbConnection.ConnectionString))
                {
                    int insertAffectedRows = await conn.ExecuteAsync(sqlInsert, movimento);

                    if (insertAffectedRows > 0)
                    {
                        res = new ResultadoBase() { Successo = true };
                    }
                    else
                    {
                        res = new ResultadoBase() { Successo = false };
                        res.MensagensDeErro.Add("Não foi realizado o insert da movimentacao.");
                    }
                }

                return res;
            }
            catch
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErro.Add("Erro ao inserir movimentacao.");
                return res;
            }
        }

        public ResultadoBase IsValidMovimento(MovimentoConta movimento)
        {

            ResultadoBase res;

            if (movimento == null)
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("INVALID_TYPE");
                return res;
            }

            if (movimento.Valor <= 0)
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE;");
                return res;
            }

            if (string.IsNullOrWhiteSpace(movimento.IdMovimento))
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("INVALID_TYPE");
                return res;
            }

            if (movimento.DataMovimento <= new DateTime(1900, 1, 1))
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("INVALID_TYPE");
                return res;
            }

            if (string.IsNullOrWhiteSpace(movimento.IdContaCorrente))
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("INVALID_TYPE");
                return res;
            }

            if (!(movimento.TipoMovimento == TipoMovimentacao.C.ToString() || movimento.TipoMovimento == TipoMovimentacao.D.ToString()))
            {
                res = new ResultadoBase() { Successo = false };
                res.MensagensDeErroValidacao.Add("Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE");
                return res;
            }

            res = new ResultadoBase() { Successo = true };
            return res;
        } 
    } 
}