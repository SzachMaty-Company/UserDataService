﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDataService.DataContext;
using UserDataService.Exceptions;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class UserService : IUserService
    {
        private readonly UserDataContext _db;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public UserService(UserDataContext db, IMapper mapper, IUserContextService userContextService)
        {
            _db = db;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            if (id == 0)
            {
                id = _userContextService.UserId == null ? 0 : (int)_userContextService.UserId;
            }

            if (id == 0) throw new UnauthorizedException();

            var user = await _db.Users.AsNoTracking()
                .Include(x => x.Statistics)
                .ThenInclude(x => x.Games)
                .FirstOrDefaultAsync(x => x.Id == id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _db.Users.AsNoTracking()
                .Include(x => x.Statistics)
                .ThenInclude(x => x.Games)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null) throw new NotFoundException();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
