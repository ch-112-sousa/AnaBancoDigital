namespace ContaCorrenteAPI.Authentication
{
    public interface IUserRepository
    {
        Task<User> GetUserForAuthentication(long numeroContaCorrente);
    }
}