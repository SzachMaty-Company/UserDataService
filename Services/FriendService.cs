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

            var friendship1 = _dbContext.Friendships
                .Include(x => x.User)
                .FirstOrDefault(x => (x.UserId == userId && x.FriendId == senderId)) ?? throw new BadRequestException("No sent");

            var friendship2 = _dbContext.Friendships
                .Include(x => x.User)
                .FirstOrDefault(x => (x.UserId == senderId && x.FriendId == userId)) ?? throw new BadRequestException("No sent");

            friendship1.IsAccepted = true;
            friendship2.IsAccepted = true;

            ChatServiceMessage chatServiceMessage = new();

            var list = new List<ChatMember>
            {
                new ChatMember
                {
                    UserId = friendship1.User.Id,
                    UserName = friendship1.User.Name,
                },
                new ChatMember
                {
                    UserId = friendship2.User.Id,
                    UserName = friendship2.User.Name,
                }
            };

            chatServiceMessage.ChatMembers.AddRange(list);

            HttpClient httpClient = new();

            var content = new StringContent(JsonConvert.SerializeObject(chatServiceMessage), Encoding.UTF8, "application/json");

            Console.WriteLine(content);

            var res = await httpClient.PostAsync("http://chatservice:8124/internal/chat", content);

            Console.WriteLine("Internal chat status code:" + res.StatusCode);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeclineFriendRequest(int userId)
        {
            var senderId = _userContextService.UserId;

            if (senderId == null) throw new UnauthorizedException();
            if (senderId == userId) throw new BadRequestException();

            var friendship1 = _dbContext.Friendships
                .FirstOrDefault(x => (x.UserId == userId && x.FriendId == senderId)) ?? throw new BadRequestException("No sent");

            var friendship2 = _dbContext.Friendships
                .FirstOrDefault(x => (x.UserId == senderId && x.FriendId == userId)) ?? throw new BadRequestException("No sent");

            _dbContext.Remove(friendship1);
            _dbContext.Remove(friendship2);

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


            var friendshipx1 = _dbContext.Friendships
                .FirstOrDefault(x => x.UserId == userId && x.FriendId == senderId);

            var friendshipx2 = _dbContext.Friendships
                .FirstOrDefault(x => x.UserId == senderId && x.FriendId == userId);

            if (friendshipx1 != null || friendshipx2 != null)
            {
                throw new BadRequestException("Already sent");
            }

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
