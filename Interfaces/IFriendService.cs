using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IFriendService
    {
        Task SendFriendRequst(int userId);
        Task AcceptFrinedRequest(int userId);
        Task DeclineFriendRequest(int userId);
        Task<IEnumerable<FriendDto>> GetFriendRequests();
        Task<IEnumerable<FriendDto>> GetFriends(int id);
    }
}
