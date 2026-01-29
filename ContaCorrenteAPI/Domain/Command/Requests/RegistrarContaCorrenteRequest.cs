
using MediatR;
using ContaCorrenteAPI.Domain.Command.Responses;

namespace ContaCorrenteAPI.Domain.Command.Requests;

public class RegistrarContaCorrenteRequest : IRequest<RegistrarContaCorrenteResponse>
{
    public long Numero { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public string Salt { get; set; }
}