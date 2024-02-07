using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;

namespace UserDataService.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }
    }
}
