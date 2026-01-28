using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public interface ICreateContaCorrenteHandler
    {
       Task<CreateContaCorrenteResponse> HandleAsync (CreateContaCorrenteRequest request);
    }
}
