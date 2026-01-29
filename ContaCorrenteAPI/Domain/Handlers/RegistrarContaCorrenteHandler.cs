using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Repositories;
using MediatR;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class RegistrarContaCorrenteHandler : IRequestHandler<RegistrarContaCorrenteRequest, RegistrarContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;        

        public RegistrarContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;            
        }

        public async Task<RegistrarContaCorrenteResponse> Handle(RegistrarContaCorrenteRequest request, CancellationToken cancellationToken)
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
                var cc = new RegistrarContaCorrenteResponse()
                {
                    Nome = request.Nome,
                    Numero = request.Numero,
                    Error = string.Empty
                }; 

                return cc;
            }

            return new RegistrarContaCorrenteResponse() { Error = "Não foi possível salvar registro." };
        }
    }
}
