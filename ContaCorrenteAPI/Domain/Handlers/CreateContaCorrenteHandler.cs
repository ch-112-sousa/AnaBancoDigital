using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Repositories;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class CreateContaCorrenteHandler : IRequestHandler<CreateContaCorrenteRequest, CreateContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;        

        public CreateContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;            
        }

        public async Task<CreateContaCorrenteResponse> Handle(CreateContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = new ContaCorrente()
            { 
                Ativo = true,
                Numero = request.Numero,
                Nome = request.Nome,
                Senha = request.Senha,
                Salt = request.Salt
            };

            bool registroSalvo = await _contaCorrenteRepository.SaveAsync(contaCorrente);

            if(registroSalvo)
            {
                var cc = new CreateContaCorrenteResponse()
                {
                    Nome = request.Nome,
                    Numero = request.Numero,
                    Error = string.Empty
                }; 

                return cc;
            }

            return new CreateContaCorrenteResponse() { Error = "Não foi possível salvar registro." };
        }
    }
}
