using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Controllers
{
    [Route("auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet("google")]
        public async Task<ActionResult<TokenDto>> GetToken([FromQuery] string code)
        {
            var token = await _service.AuthenticateAsync(code);
            return Ok(token);
        }
    }
}
