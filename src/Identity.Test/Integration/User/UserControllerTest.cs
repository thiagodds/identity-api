using Identity.Api;
using Identity.Model.Api.Dto.User;
using Identity.Test.Core;
using Identity.Test.Core.Model;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Identity.Test.Integration.User
{
    public class UserControllerTest : IClassFixture<ApplicationFactory<Startup>>
    {
        private readonly ApplicationFactory<Startup> _factory;

        public UserControllerTest(ApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateNewUser_EmptyFields()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/user", new UserDto
            {

            });

            Assert.False(response.IsSuccessStatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsObject<ErrorResponse>();

                Assert.Equal(4, responseData.Errors.Count);
                Assert.Equal("The Email field is required.", responseData.Errors["Email"].Single());
                Assert.Equal("The LastName field is required.", responseData.Errors["LastName"].Single());
                Assert.Equal("The Password field is required.", responseData.Errors["Password"].Single());
                Assert.Equal("The FirstName field is required.", responseData.Errors["FirstName"].Single());
            }
        }

        [Fact]
        public async Task CreateNewUser_DuplicatedEmail()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/user", new UserDto
            {
                Email = "admin@email.com",
                FirstName = "FirstName",
                Password = "Password",
                LastName = "LastName"
            });

            Assert.False(response.IsSuccessStatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsObject<ErrorResponse>();

                Assert.Single(responseData.Errors);
                Assert.NotNull(responseData.Errors["Email"]);
            }
        }

        [Fact]
        public async Task CreateNewUser_InvalidPassword()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/user", new UserDto
            {
                Email = "email@email.com",
                FirstName = "FirstName",
                Password = "Password",
                LastName = "LastName"
            });

            Assert.False(response.IsSuccessStatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsObject<ErrorResponse>();

                var errorsKeys = responseData.Errors.Select(x => x.Key);
                Assert.Contains<string>("PasswordRequiresNonAlphanumeric", errorsKeys);
                Assert.Contains<string>("PasswordRequiresDigit", errorsKeys);
            }
        }
        
        [Fact]
        public async Task CreateNewUser_Success()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.PostAsJsonAsync("/api/user", new UserDto
            {
                Email = "email@email.com",
                FirstName = "FirstName",
                Password = "123qweEWQ#@!",
                LastName = "LastName"
            });

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
