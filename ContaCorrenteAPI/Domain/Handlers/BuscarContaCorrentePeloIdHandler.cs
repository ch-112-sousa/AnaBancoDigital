using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Queries.Requests;
using ContaCorrenteAPI.Domain.Queries.Responses;
using ContaCorrenteAPI.Repositories;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class BuscarContaCorrentePeloIdHandler : IRequestHandler<BuscarContaCorrentePeloIdRequest, BuscarContaCorrentePeloIdResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public BuscarContaCorrentePeloIdHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<BuscarContaCorrentePeloIdResponse> Handle(BuscarContaCorrentePeloIdRequest request, CancellationToken cancellationToken)
        {
            ContaCorrente cc = await _contaCorrenteRepository.ObterContaCorrentePeloIdAsync(request.IdContaCorrente);

            var response = new BuscarContaCorrentePeloIdResponse()
            {
                IdContaCorrente = cc.IdContaCorrente,
                Ativo = cc.Ativo,
                Numero = cc.Numero,
                Nome = cc.Nome
            };

            return response;
        }
    }
}