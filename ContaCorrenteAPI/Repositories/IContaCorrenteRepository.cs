using ContaCorrenteAPI.Domain.Entities;

namespace ContaCorrenteAPI.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> GetContaCorrenteByIdAsync(string idContaCorrente);
        Task<bool> SaveAsync(ContaCorrente contaCorrente);
    }
}