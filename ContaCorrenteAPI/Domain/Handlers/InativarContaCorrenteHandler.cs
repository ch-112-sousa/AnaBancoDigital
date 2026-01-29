using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Queries.Requests;
using ContaCorrenteAPI.Domain.Queries.Responses;
using ContaCorrenteAPI.Repositories;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class InativarContaCorrenteHandler : IRequestHandler<InativarContaCorrenteRequest, InativarContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public InativarContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<FindContaCorrenteByIdResponse> Handle(FindContaCorrenteByIdRequest request, CancellationToken cancellationToken)
        {
            ContaCorrente cc = await _contaCorrenteRepository.GetContaCorrenteByIdAsync(request.IdContaCorrente);

            var response = new FindContaCorrenteByIdResponse()
            {
                IdContaCorrente = cc.IdContaCorrente,
                Ativo = cc.Ativo,
                Numero = cc.Numero,
                Nome = cc.Nome
            };

            return response;
        }

        public async Task<InativarContaCorrenteResponse> Handle(InativarContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            string msgErro = await _contaCorrenteRepository.InativarContaCorrentePeloNumeroAsync(request.NumeroContaCorrente, request.Senha);

            var response = new InativarContaCorrenteResponse();
            response.Error = msgErro;

            return response;
        }
    }
}
