namespace AuthApi.Domain
{
    public interface IUsersRepository
    {
        UserEntity GetUser(string userName);
    }
}
