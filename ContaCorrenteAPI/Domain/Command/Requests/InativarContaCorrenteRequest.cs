using ContaCorrenteAPI.Domain.Command.Responses;
using MediatR;

namespace ContaCorrenteAPI.Domain.Command.Requests
{
    public class InativarContaCorrenteRequest : IRequest<InativarContaCorrenteResponse>
    {
        public long NumeroContaCorrente { get; set; }
        public string Senha { get; set; }

    }
}
