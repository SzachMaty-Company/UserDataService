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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            var user = await _service.GetUserById(id);
            return user;
        }

        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserByName([FromQuery] string name)
        {
            var user = await _service.GetUserByName(name);
            return user;
        }
    }
}
