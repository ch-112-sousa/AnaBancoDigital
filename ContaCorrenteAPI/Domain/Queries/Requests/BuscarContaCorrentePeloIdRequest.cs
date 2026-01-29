using ContaCorrenteAPI.Domain.Queries.Responses;
using MediatR;

namespace ContaCorrenteAPI.Domain.Queries.Requests
{
    public class BuscarContaCorrentePeloIdRequest : IRequest<BuscarContaCorrentePeloIdResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}