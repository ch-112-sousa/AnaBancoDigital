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
            try
            {
                var contaCorrente = new ContaCorrente()
                {
                    Ativo = true,
                    Numero = request.Numero,
                    Nome = request.Nome,
                    Senha = request.Senha,
                    Salt = request.Salt
                };

                var res = await _contaCorrenteRepository.SalvarRegistroAsync(contaCorrente);

                if (res.Successo)
                {
                    var contaResponse = new RegistrarContaCorrenteResponse()
                    {
                        Nome = request.Nome,
                        Numero = request.Numero,
                        Info = res
                    };

                    return contaResponse;
                }

                var response = new RegistrarContaCorrenteResponse() { Info = res };
                return response;
            }
            catch
            {
                var response = new RegistrarContaCorrenteResponse();
                response.Info = new Models.ResultadoBase() { Successo = false };
                response.Info.MensagensDeErro.Add("Erro ao registrar uma conta corrente(handler).");
                return response;
            }
        }
    }
}