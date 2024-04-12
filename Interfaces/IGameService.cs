using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IGameService
    {
        Task SaveGameAsync(CreateGameDto createGameDto);
    }
}
