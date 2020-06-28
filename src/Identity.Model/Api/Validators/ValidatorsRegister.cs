using Identity.Model.Api.Validators.User;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Model.Api.Validators
{
    public static class ValidatorsRegister
    {
        public static void RegisterValidators(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUserValidator, UserValidator>();
        }
    }
}
