using Identity.Model.Api.Dto.User;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Identity.Api.Services.User
{
    public interface IUserService
    {
        Task<IdentityResult> CreateNewUser(UserDto userDto);
    }
}
