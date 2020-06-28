using Identity.Model.Api.Dto.User;
using Identity.Model.Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Services.User
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<IdentityResult> CreateNewUser(UserDto userDto)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser
                         {
                            Email = userDto.Email,
                            UserName = userDto.Email,
                            FirstName = userDto.FirstName,
                            LastName = userDto.LastName
                         }, userDto.Password);

            return result;
        }
    }
}
