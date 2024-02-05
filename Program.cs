using System;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("auth/google", async (HttpContext httpContext, IHttpClientFactory httpClientFactory) => 
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
    using var res = await httpClient.SendAsync(req);

    var content = await res.Content.ReadAsStringAsync();
    var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(content);

    var jwtHandler = new JwtSecurityTokenHandler();

    var googleClaims = jwtHandler.ReadJwtToken(token.id_token);

    
    return Results.Ok(token);
});

app.Run();

class Token
{
    public string access_token { get; set; }
    public string expires_in { get; set; }
    public string id_token { get; set; }
}