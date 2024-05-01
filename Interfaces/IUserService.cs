using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(int id);
        public Task<UserDto> GetUserByEmail(string email);
    }
}
