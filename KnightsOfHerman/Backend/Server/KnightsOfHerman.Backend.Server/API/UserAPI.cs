using KnightsOfHerman.Backend.Common.User.Abstract;
using KnightsOfHerman.Common.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightsOfHerman.Backend.Server.API
{
    /// <summary>
    /// User Account Controller
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class UserAPIController : ControllerBase
    {
        IUserService _userService;
        public UserAPIController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Attempts to register a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            var result = await _userService.TryCreateUserAsync(model);
            if (result.IsSuccess)
            {
                return Ok(new JwtToken(result.Value));
            }
            else
            {
                return Unauthorized(result.ErrorMessage); //Send more descriptive response
            }
        }

        /// <summary>
        /// Attempts to login
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel credentials)
        {
            var result = await _userService.TryGetJWTTokenAsync(credentials);
            if (result.IsSuccess)
            {
                return Ok(new JwtToken(result.Value));
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Just a test Post to see if a token is authorized
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("checktoken")]
        public IActionResult CheckToken()
        {
            return Ok();
        }

        /// <summary>
        /// Returns a list of errors with the username, if empty then no errors.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("checkusername")]
        public async Task<IActionResult> CheckUsernameAsync([FromBody] string username)
        {
            var errors = await _userService.CheckUsername(username);
            return Ok(errors);
        }


        /// <summary>
        /// Returns a list of errors with the email, if empty then no errors.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("checkemail")]
        public async Task<IActionResult> CheckEmailAsync([FromBody] string email)
        {
            var errors = await _userService.CheckEmail(email);
            return Ok(errors);
        }

        /// <summary>
        /// Returns a list of errors with the username, if empty then no errors.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("lookforusername")]
        public async Task<IActionResult> LookForUsernameAsync([FromBody] string username)
        {
            var found = await _userService.LookForUsername(username);
            return Ok(found);
        }

    }
}
