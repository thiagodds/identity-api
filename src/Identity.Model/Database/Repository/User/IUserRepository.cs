namespace Identity.Model.Database.Repository.User
{
    public interface IUserRepository
    {
        bool HasUserByEmail(string email);
    }
}
