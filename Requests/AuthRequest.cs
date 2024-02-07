using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserDataService.Models;

namespace UserDataService.Requests
{
    public static class AuthRequest
    {
        public static WebApplication RegisterAuthEndpoints(this WebApplication app)
        {
            app.MapGet("auth/google", AuthRequest.GetToken);
            app.MapGet("protected", AuthRequest.Protected);
            return app;
        }

        public static IResult Protected(HttpContext httpContext)
        {
            var auth = httpContext.Request.Headers.Authorization;
            return Results.Ok(auth);
        }

        public static IResult GetToken(HttpContext httpContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            var code = httpContext.Request.Query["code"];
       
            if (string.IsNullOrEmpty(code))
            {
                return Results.Unauthorized();
            }

            var clientId = configuration["Oauth:Google:ClientId"];
            var clientSecret = configuration["Oauth:Google:ClientSecret"];

            var form = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", "http://localhost:3000"),
                new KeyValuePair<string, string>("grant_type", "authorization_code")
            };

            var url = "https://oauth2.googleapis.com/token";
            using var httpClient = httpClientFactory.CreateClient();
            using var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(form) };
            using var res = httpClient.SendAsync(req).GetAwaiter().GetResult();

            var content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content);

            var jwtHandler = new JwtSecurityTokenHandler();

            var tokenContent = jwtHandler.ReadJwtToken(token.id_token);

            var userEmail = tokenContent.Claims.First(x => x.Type == "email").Value;
            var userName = tokenContent.Claims.First(x => x.Type == "name").Value;
            var subject = tokenContent.Claims.First(x => x.Type == "sub").Value;

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.NameIdentifier, subject)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(20);

            var jwtBearer = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials,
                issuer: issuer,
                audience: audience 
                );

            return Results.Ok(jwtHandler.WriteToken(jwtBearer));
        }
    }
}
