using AutoMapper;
using UserDataService.DataContext.Entities;
using UserDataService.Models;

namespace UserDataService
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
