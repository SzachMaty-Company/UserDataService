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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService service, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _service = service;
            _contextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("google")]
        public async Task<ActionResult<TokenDto>> GetToken()
        {
            var token = await _service.AuthenticateAsync(_contextAccessor.HttpContext!, _httpClientFactory, _configuration);
            return Ok(token);
        }

        [HttpGet("protected")]
        public async Task<ActionResult<TokenDto>> Protected()
        {
            var token = await _service.GetTokenAsync(_contextAccessor.HttpContext!);
            return Ok(token);
        }
    }
}
