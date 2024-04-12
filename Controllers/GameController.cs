using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;
using UserDataService.Models;

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

        [HttpPost]
        public async Task<ActionResult> SaveGame([FromBody] CreateGameDto createGameDto)
        {
            await _service.SaveGameAsync(createGameDto);
            return Ok();
        }
    }
}
