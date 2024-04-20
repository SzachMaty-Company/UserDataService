using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using UserDataService.DataContext;
using UserDataService.DataContext.Entities;
using UserDataService.Exceptions;
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

            if (senderId == null) throw new UnauthorizedException();
            if (senderId == userId) throw new BadRequestException();

            var friendships = _dbContext.Friendships
                .Where(x => (x.UserId == userId && x.FriendId == senderId)
                    || (x.UserId == senderId && x.FriendId == userId));

            if (friendships.Count() != 2) throw new BadRequestException();

            await friendships.ForEachAsync(x => x.IsAccepted = true);

            ChatServiceMessage chatServiceMessage = new();

            var list = new List<ChatMember>();

            list.Add(new ChatMember
            {
                UserId = friendships.First(x => x.UserId == senderId).User.Id,
                UserName = friendships.First(x => x.UserId == senderId).User.Name,
            });

            list.Add(new ChatMember
            {
                UserId = friendships.First(x => x.UserId == userId).User.Id,
                UserName = friendships.First(x => x.UserId == userId).User.Name,
            });

            chatServiceMessage.ChatMembers.AddRange(list);

            HttpClient httpClient = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(chatServiceMessage), Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync("http://localhost:8124/internal/chat", content);

            Console.WriteLine("Internal chat status code:" + res.StatusCode);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeclineFriendRequest(int userId)
        {
            var senderId = _userContextService.UserId;

            if (senderId == null) throw new UnauthorizedException();
            if (senderId == userId) throw new BadRequestException();

            var friendships = _dbContext.Friendships
                .Where(x => (x.UserId == userId && x.FriendId == senderId)
                    || (x.UserId == senderId && x.FriendId == userId));

            if (friendships.Count() != 2) throw new BadRequestException();

            await friendships.ForEachAsync(x => _dbContext.Remove(x));

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendDto>> GetFriendRequests()
        {
            var userId = _userContextService.UserId;

            if (userId == null) throw new UnauthorizedException();

            var requests = await _dbContext.Friendships
                .AsNoTracking()
                .Where(x => (x.UserId == userId && x.IsAccepted == false && x.SentBy != userId))
                .Select(x => x.Friend)
                .ToListAsync();

            var requestDtos = _mapper.Map<IEnumerable<FriendDto>>(requests);
            return requestDtos;
        }

        public async Task<IEnumerable<FriendDto>> GetFriends(int id)
        {
            var userId = id;
            if (userId == 0)
            {
                userId = (int)_userContextService.UserId;
            }

            var friends = await _dbContext.Friendships
                .AsNoTracking()
                .Where(x => (x.UserId == userId && x.IsAccepted == true))
                .OrderBy(x => x.FriendId)
                .Select(x => x.Friend)
                .ToListAsync();

            var winrates = await _dbContext.Friendships
                .AsNoTracking()
                .Where(x => (x.UserId == userId && x.IsAccepted == true))
                .OrderBy(x => x.FriendId)
                .Select(x => x.WinrateAgainst)
                .ToListAsync();

            var friendDtos = _mapper.Map<IEnumerable<FriendDto>>(friends);
            for (int i = 0; i < friendDtos.Count(); i++)
            {
                ((List<FriendDto>)friendDtos)[i].WinrateAgainst = winrates[i];
            }

            return friendDtos;
        }

        public async Task SendFriendRequst(int userId)
        {
            var senderId = _userContextService.UserId;

            if (senderId == null) throw new UnauthorizedException();
            if (senderId == userId) throw new BadRequestException();

            var friendship1 = new Friendship()
            {
                FriendId = (int)senderId,
                UserId = userId,
                IsAccepted = false,
                SentBy = (int)senderId
            };
            var friendship2 = new Friendship()
            {
                FriendId = userId,
                UserId = (int)senderId,
                IsAccepted = false,
                SentBy = (int)senderId
            };

            await _dbContext.AddAsync(friendship1);
            await _dbContext.AddAsync(friendship2);
            await _dbContext.SaveChangesAsync();
        }
    }
}
