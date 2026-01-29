using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Models;

namespace ContaCorrenteAPI.Repositories
{
    public interface IMovimentacaoRepository
    {
        Task<ResultadoBase> RegistrarMovimentacaoAsync(MovimentoConta movimento);

    }
}
