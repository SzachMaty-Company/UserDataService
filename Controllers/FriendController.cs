using Microsoft.AspNetCore.Mvc;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Controllers
{
    [ApiController]
    [Route("friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriends([FromQuery] int id)
        {
            var friends = await _service.GetFriends(id);
            return Ok(friends);
        }

        [HttpGet("request")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetRequests()
        {
            var requests = await _service.GetFriendRequests();
            return Ok(requests);
        }

        [HttpPost("{userId}/send")]
        public async Task<ActionResult> SendFriendRequest([FromRoute] int userId)
        {
            await _service.SendFriendRequst(userId);
            return Created();
        }

        [HttpPost("{userId}/accept")]
        public async Task<ActionResult> AcceptFriendRequest([FromRoute] int userId)
        {
            await _service.AcceptFrinedRequest(userId);
            return Accepted();
        }

        [HttpPost("{userId}/decline")]
        public async Task<ActionResult> DeclineFriendRequest([FromRoute] int userId)
        {
            await _service.DeclineFriendRequest(userId);
            return Accepted();
        }
    }
}
