using KnightsOfHerman.Common.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnightsOfHerman.Backend.Server.API
{
    /// <summary>
    /// API Controller to just test connection
    /// </summary>
    [Route("[Controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpPost("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }

        [HttpPost("pingstring")]
        public string PingString()
        {
            return "Pong";
        }
    }
}
