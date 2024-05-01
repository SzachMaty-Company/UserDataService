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
            CreateMap<Statistics, StatisticsDto>();
            CreateMap<Game, GameDto>();
            CreateMap<User, FriendDto>();
            CreateMap<CreateGameDto, Game>()
                .ForMember(dest => dest.BlackId, opt => opt.MapFrom(src => src.blackUserId))
                .ForMember(dest => dest.WhiteId, opt => opt.MapFrom(src => src.whiteUserId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(src.gameStartTime), TimeZoneInfo.Local)))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.gameMode))
                .ForMember(dest => dest.Win, opt => opt.MapFrom(src => src.gameStatus))
                .ForMember(dest => dest.Moves, opt => opt.MapFrom(src => src.moveList));
        }
    }
}
