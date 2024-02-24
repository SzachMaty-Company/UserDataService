using UserDataService.DataContext;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class GameService : IGameService
    {
        private readonly UserDataContext _db;

        public GameService(UserDataContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<GameDto>> GetAllUserGames()
        {
            throw new NotImplementedException();
        }
    }
}
