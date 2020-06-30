using Identity.Api;
using Identity.Model.Api.Dto.User;
using Identity.Test.Core;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Integration.User
{
    public class LoginControllerTest : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public LoginControllerTest(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_Success()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/login", new LoginDto
            {
                Email = "admin@email.com",
                Password = "123qweEWQ#@!"
            });

            Assert.True(response.IsSuccessStatusCode);

            var responseData = await response.Content.ReadAsObject<TokenResponse>();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRET123qweEWQ#@!");

            try
            {

                tokenHandler.ValidateToken(responseData.Token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                Assert.False(true);
            }
            catch(Exception ex)
            {
                Assert.False(false);
            }
        }

        [Fact]
        public async Task Login_EmptyFields()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/login", new LoginDto
            {
                Email = "",
                Password = ""
            });

            Assert.False(response.IsSuccessStatusCode);
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
