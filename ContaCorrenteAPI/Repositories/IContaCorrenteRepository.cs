using ContaCorrenteAPI.Domain.Entities;

namespace ContaCorrenteAPI.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetContaCorrenteByIdAsync(string idContaCorrente);
        Task<bool> SalvarRegistroAsync(ContaCorrente contaCorrente);
        Task<string> InativarContaCorrentePeloNumeroAsync(long numeroContaCorrente, string senha);
    }
}