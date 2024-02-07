﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using UserDataService.Interfaces;
using UserDataService.Models;

namespace UserDataService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Task<TokenDto> AuthenticateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var code = httpContext.Request.Query["code"];

            if (string.IsNullOrEmpty(code))
            {
                return Task.FromResult(new TokenDto());
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
            using var res = httpClient.SendAsync(req).GetAwaiter().GetResult();

            var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content);

            var jwtHandler = new JwtSecurityTokenHandler();

            var tokenContent = jwtHandler.ReadJwtToken(token.id_token);

            var userEmail = tokenContent.Claims.First(x => x.Type == "email").Value;
            var userName = tokenContent.Claims.First(x => x.Type == "name").Value;
            var subject = tokenContent.Claims.First(x => x.Type == "sub").Value;

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.NameIdentifier, subject)
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

            return Task.FromResult(new TokenDto { Token = jwtHandler.WriteToken(jwtBearer) });
        }

        public Task<TokenDto> GetTokenAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Headers.Authorization;
            return Task.FromResult(new TokenDto()
            {
                Token = token
            });
        }
    }
}
