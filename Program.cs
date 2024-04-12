using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using UserDataService;
using UserDataService.DataContext;
using UserDataService.Interfaces;
using UserDataService.Middlewares;
using UserDataService.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authenticationProviderKey = "JwtBearer";
builder.Services
    .AddAuthentication()
    .AddJwtBearer(authenticationProviderKey,
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = false,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

builder.Services.AddAuthorization();

//HttpClient
builder.Services.AddHttpClient();

//Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IGameService, GameService>();

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//DbContext
builder.Services.AddDbContext<UserDataContext>(opt =>
        opt.UseNpgsql(configuration.GetConnectionString("Default")));

using (var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<UserDataContext>())
{
    dbContext.Database.Migrate();
    if (!dbContext.Users.Any())
    {
        dbContext.Seed();
    }
}

//Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Middlewares
builder.Services.AddScoped<ErrorHandler>();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseMiddleware<ErrorHandler>();

app.Run();