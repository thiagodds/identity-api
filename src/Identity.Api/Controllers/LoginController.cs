using Identity.Api.Services.User;
using Identity.Model.Api.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _userService.Login(loginDto);
                return Ok(new { token });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
