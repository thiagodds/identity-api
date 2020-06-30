using Identity.Model.Database.Models;

namespace Identity.Model.Database.Repository.User
{
    public interface IUserRepository
    {
        bool HasUserByEmail(string email);
        ApplicationUser GetUserByEmail(string email);
    }
}
