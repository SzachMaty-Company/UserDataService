using UserDataService.Models;

namespace UserDataService.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> AuthenticateAsync(HttpContext httpContext, IHttpClientFactory httpClientFactory, IConfiguration configuration);
        Task<TokenDto> GetTokenAsync(HttpContext httpContext);
    }
}
