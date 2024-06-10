using AkaShop.Application.Users;
using AkaShop.Application.Users.Requests;
using AkaShop.Auth;
using AkaShop.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AkaShop.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IOptions<JWTConfiguration> options) : BaseController
    {
        private readonly IOptions<JWTConfiguration> _options = options;
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetProfile(UserId));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserModel loginModel, CancellationToken cancellationToken)
        {
            var authResponse = await _userService.Login(loginModel);
            if (authResponse != null)
            {
                return Ok(JWTHelper.GenerateSecurityToken(authResponse, _options));
            }
            else return BadRequest("Invalid login attempt");
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserModel model,CancellationToken cancellationToken)
        {
            var authResponse = await _userService.CreateUserAsync(model);
            if (authResponse != null)
            {
                return Ok(JWTHelper.GenerateSecurityToken(authResponse, _options));
            }
            else return BadRequest("Invalid login attempt");
        }
    }
}
