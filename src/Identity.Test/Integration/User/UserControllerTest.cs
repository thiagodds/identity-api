using Identity.Api;
using Identity.Model.Api.User;
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
    }
}
