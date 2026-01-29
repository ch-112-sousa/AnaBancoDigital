using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Models;
using ContaCorrenteAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class RegistrarMovimentacaoHandler : IRequestHandler<RegistrarMovimentacaoRequest, RegistrarMovimentacaoResponse>
    {

        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegistrarMovimentacaoHandler(IContaCorrenteRepository contaCorrenteRepository, IMovimentacaoRepository movimentacaoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
            _movimentacaoRepository = movimentacaoRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<RegistrarMovimentacaoResponse> Handle(RegistrarMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            var response = new RegistrarMovimentacaoResponse();
            var conta = await _contaCorrenteRepository.ObterContaCorrentePeloNumeroAsync(request.NumeroContaCorrente);
            if (conta != null)
            {
                var context = _httpContextAccessor.HttpContext;
                long.TryParse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out long numeroConta);

                if (numeroConta > 0 && numeroConta != request.NumeroContaCorrente && request.TipoMovimentacao == TipoMovimentacao.D.ToString())
                {
                    response.Info = new Models.ResultadoBase() { Successo = false };
                    response.Info.MensagensDeErroValidacao.Add("Apenas o tipo “crédito” pode ser aceito caso o número da conta seja diferente do usuário logado; TIPO: INVALID_TYPE");
                    return response;
                }


                if (conta.Ativo)
                {
                    var movimentacao = new MovimentoConta()
                    {
                        IdMovimento = Guid.NewGuid().ToString(),
                        IdContaCorrente = conta.IdContaCorrente,
                        DataMovimento = DateTime.Now,
                        TipoMovimento = request.TipoMovimentacao,
                        Valor = request.Valor
                    };

                    var info = await _movimentacaoRepository.RegistrarMovimentacaoAsync(movimentacao);
                    response.Info = info;
                    return response;
                }

                response.Info = new Models.ResultadoBase() { Successo = false };
                response.Info.MensagensDeErroValidacao.Add("Apenas contas correntes ativas podem receber movimentação; TIPO: INACTIVE_ACCOUNT");
                return response;
            }

            response.Info = new Models.ResultadoBase() { Successo = false };
            response.Info.MensagensDeErroValidacao.Add("Apenas contas correntes cadastradas podem receber movimentação; TIPO: INVALID_ACCOUNT");
            return response;
        }
    }
}