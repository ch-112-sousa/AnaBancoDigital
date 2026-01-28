using ContaCorrenteAPI.Domain.Queries.Responses;
using MediatR;

namespace ContaCorrenteAPI.Domain.Queries.Requests
{
    public class FindContaCorrenteByIdRequest : IRequest<FindContaCorrenteByIdResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}