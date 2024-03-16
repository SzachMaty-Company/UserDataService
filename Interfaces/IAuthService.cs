using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> AuthenticateAsync(string code);
    }
}
