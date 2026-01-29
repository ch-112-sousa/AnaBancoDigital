namespace ContaCorrenteAPI.Authentication
{
    public interface IUserRepository
    {
        Task<User> GetUserForAuthentication(long numeroContaCorrente);

        Task<bool> SenhaValida(long numeroContaCorrente, string senhaInput);
    }
}