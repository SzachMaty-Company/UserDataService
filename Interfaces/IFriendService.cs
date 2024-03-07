using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IFriendService
    {
        Task SendFriendRequst(int userId);
        Task AcceptFrinedRequest(int userId);
        Task<IEnumerable<UserDto>> GetFriendRequests();

        Task<IEnumerable<UserDto>> GetFriends();
    }
}
