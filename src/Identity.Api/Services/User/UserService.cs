using Identity.Model.Api.Dto.User;
using Identity.Model.Database.Models;
using Identity.Model.Database.Repository.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Services.User
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IUserRepository _userRepository;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentException(nameof(signInManager));
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
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

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null)
            {
                throw new ApplicationException("Invalid email or password");
            }

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!loginResult.Succeeded)
            {
                throw new ApplicationException("Invalid email or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRET123qweEWQ#@!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"{ user.FirstName } { user.LastName }"),
                    new Claim(ClaimTypes.Email, user.Email),
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
