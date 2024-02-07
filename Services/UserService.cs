using UserDataService.DataContext;
using UserDataService.Interfaces;

namespace UserDataService.Services
{
    public class UserService : IUserService
    {
        private readonly UserDataContext _db;
        public UserService(UserDataContext db)
        {
             _db = db;
        }
    }
}
