using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class RegistrrarMovimentacaoHandler : IRequestHandler<RegistrarMovimentacaoRequest, RegistrarMovimentacaoResponse>
    {
        public async Task<RegistrarMovimentacaoResponse> Handle(RegistrarMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            
        }
    }
}
