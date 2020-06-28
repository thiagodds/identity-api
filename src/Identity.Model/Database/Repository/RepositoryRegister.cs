using Identity.Model.Database.Repository.User;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Model.Database.Repository
{
    public static class RepositoryRegister
    {
        public static void RegisterRepositories(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
