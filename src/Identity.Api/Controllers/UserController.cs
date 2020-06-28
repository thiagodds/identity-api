using Identity.Api.Core;
using Identity.Api.Services.User;
using Identity.Model.Api.Dto.User;
using Identity.Model.Api.Validators.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserValidator _userValidator;
        private IUserService _userService;

        public UserController(IUserValidator userValidator, IUserService userService)
        {
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            _userValidator.ValidateEmail(ModelState, userDto.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.BadRequest());
            }

            var newUserResult = await _userService.CreateNewUser(userDto);
            if (!newUserResult.Succeeded)
            {
                ModelState.AddIdentityErrors(newUserResult);
                return BadRequest(ModelState.BadRequest());
            }

            return Ok();
        }
    }
}
