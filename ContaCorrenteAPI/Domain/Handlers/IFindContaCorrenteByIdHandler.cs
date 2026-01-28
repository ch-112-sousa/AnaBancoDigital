using ContaCorrenteAPI.Domain.Queries.Requests;
using ContaCorrenteAPI.Domain.Queries.Responses;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public interface IFindContaCorrenteByIdHandler
    {
        FindContaCorrenteByIdResponse Handle(FindContaCorrenteByIdRequest request);
    }
}
