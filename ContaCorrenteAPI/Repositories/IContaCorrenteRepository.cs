using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Models;

namespace ContaCorrenteAPI.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> ObterContaCorrentePeloIdAsync(string id);
        Task<ResultadoBase> SalvarRegistroAsync(ContaCorrente contaCorrente);
        Task<ResultadoBase> InativarContaCorrentePeloNumeroAsync(long numeroContaCorrente, string senha);
        Task<ResultadoBase> ExistsContaCorrenteByNumeroAsync(long numeroContaCorrente);
        Task<ContaCorrente?> ObterContaCorrentePeloNumeroAsync(long numeroContaCorrente);
        Task<ContaCorrente> ObterContaCorrentePeloNumeroENomeAsync(long numero, string nome);
    }
}