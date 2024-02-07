using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;

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
    }
}
