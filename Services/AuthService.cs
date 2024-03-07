using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserDataService.DataContext;
using UserDataService.DataContext.Entities;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly UserDataContext _db;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserDataContext db)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _db = db;
        }

        public async Task<TokenDto> AuthenticateAsync(string code)
        {

            if (string.IsNullOrEmpty(code))
            {
                return new TokenDto();
            }

            var clientId = _configuration["Oauth:Google:ClientId"];
            var clientSecret = _configuration["Oauth:Google:ClientSecret"];

            var form = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", "http://localhost:3000"),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            };

            var url = "https://oauth2.googleapis.com/token";
            using var httpClient = _httpClientFactory.CreateClient();
            using var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(form) };
            using var res = await httpClient.SendAsync(req);

            var content = await res.Content.ReadAsStringAsync();
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content);

            var jwtHandler = new JwtSecurityTokenHandler();

            var tokenContent = jwtHandler.ReadJwtToken(token.id_token);

            var userEmail = tokenContent.Claims.First(x => x.Type == "email").Value;
            var userName = tokenContent.Claims.First(x => x.Type == "name").Value;
            var subject = tokenContent.Claims.First(x => x.Type == "sub").Value;

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user == null)
            {
                user = new User
                {
                    Surname = "",
                    Name = userName,
                    Email = userEmail,
                };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(20);

            var jwtBearer = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials,
                issuer: issuer,
                audience: audience
                );

            return new TokenDto { Token = jwtHandler.WriteToken(jwtBearer) };
        }
    }
}
