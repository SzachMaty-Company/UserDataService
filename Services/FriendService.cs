using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDataService.DataContext;
using UserDataService.DataContext.Entities;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class FriendService : IFriendService
    {
        private readonly IUserContextService _userContextService;
        private readonly UserDataContext _dbContext;
        private readonly IMapper _mapper;

        public FriendService(UserDataContext dbContext,
            IUserContextService userContextService,
            IMapper mapper)
        {
            _userContextService = userContextService;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task AcceptFrinedRequest(int userId)
        {
            var senderId = _userContextService.UserId;

            if (senderId == null) throw new Exception();
            if (senderId == userId) throw new Exception();

            var friendships = _dbContext.Friendships
                .Where(x => (x.UserId == userId && x.FriendId == senderId) || (x.UserId == senderId && x.FriendId == userId));

            await friendships.ForEachAsync(x => x.IsAccepted = true);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetFriendRequests()
        {
            var userId = _userContextService.UserId;
            var requests = await _dbContext.Friendships.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.Friend).ToListAsync();

            var requestDtos = _mapper.Map<IEnumerable<UserDto>>(requests);
            return requestDtos;
        }

        public async Task SendFriendRequst(int userId)
        {
            var senderId = _userContextService.UserId;

            if (senderId == null) throw new Exception();
            if (senderId == userId) throw new Exception();

            var friendship1 = new Friendship()
            {
                FriendId = (int)senderId,
                UserId = userId,
                IsAccepted = false,
            };
            var friendship2 = new Friendship()
            {
                FriendId = userId,
                UserId = (int)senderId,
                IsAccepted = false
            };

            await _dbContext.AddAsync(friendship1);
            await _dbContext.AddAsync(friendship2);
            await _dbContext.SaveChangesAsync();
        }
    }
}
