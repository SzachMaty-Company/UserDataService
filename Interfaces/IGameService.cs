using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAllUserGames();
    }
}
