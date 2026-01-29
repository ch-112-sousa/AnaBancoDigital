using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Queries.Requests;
using ContaCorrenteAPI.Domain.Queries.Responses;
using ContaCorrenteAPI.Repositories;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class FindContaCorrenteByIdHandler : IRequestHandler<FindContaCorrenteByIdRequest, FindContaCorrenteByIdResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public FindContaCorrenteByIdHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<FindContaCorrenteByIdResponse> Handle(FindContaCorrenteByIdRequest request, CancellationToken cancellationToken)
        {
            // TODO: Lógica de leitura se houver

            // Retorna o resultado
            //var result = _contaCorrenteRepository.GetContaCorrenteById(request.IdContaCorrente);
            //return Task.FromResult(result);
            
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
    }
}
