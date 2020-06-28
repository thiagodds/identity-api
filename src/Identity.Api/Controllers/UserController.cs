using Identity.Model.Api.User;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] UserDto userDto)
        {
            return Ok(userDto.FirstName);
        }
    }
}
