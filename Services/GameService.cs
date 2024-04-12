using AutoMapper;
using UserDataService.DataContext;
using UserDataService.DataContext.Entities;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class GameService : IGameService
    {
        private readonly UserDataContext _db;
        private readonly IMapper _mapper;

        public GameService(UserDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task SaveGameAsync(CreateGameDto createGameDto)
        {
            var game = _mapper.Map<Game>(createGameDto);
            await _db.AddAsync(game);
            await _db.SaveChangesAsync();
        }
    }
}
