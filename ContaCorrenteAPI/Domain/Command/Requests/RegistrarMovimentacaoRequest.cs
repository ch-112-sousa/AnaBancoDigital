using ContaCorrenteAPI.Domain.Command.Responses;
using MediatR;

namespace ContaCorrenteAPI.Domain.Command.Requests
{
    public class RegistrarMovimentacaoRequest : IRequest<RegistrarMovimentacaoResponse>
    {

        public long NumeroContaCorrente { get; set; }

        public string TipoMovimentacao { get; set; }
      
    }
}
