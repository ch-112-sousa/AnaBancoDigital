using ContaCorrenteAPI.Authentication;

namespace ContaCorrenteAPI.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel> ObterUsuarioParaAutenticacao(long numeroContaCorrente);

        Task<bool> SenhaValida(long numeroContaCorrente, string senhaInput);

        string HashPassword(string senha);
    }
}