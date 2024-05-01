using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserById([FromQuery] int id)
        {
            var user = await _service.GetUserById(id);
            return Ok(user);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUserByName([FromRoute] string email)
        {
            var user = await _service.GetUserByEmail(email);
            return Ok(user);
        }
    }
}
