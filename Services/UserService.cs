using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDataService.DataContext;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class UserService : IUserService
    {
        private readonly UserDataContext _db;
        private readonly IMapper _mapper;

        public UserService(UserDataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByName(string name)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Surname == name);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
