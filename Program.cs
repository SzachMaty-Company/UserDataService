using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserDataService.DataContext;
using UserDataService.Interfaces;
using UserDataService.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//HttpClient
builder.Services.AddHttpClient();

//Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//DbContext
builder.Services.AddDbContext<UserDataContext>(opt =>
        opt.UseNpgsql(configuration.GetConnectionString("Default")));

using (var dbContext = builder.Services.BuildServiceProvider().GetRequiredService<UserDataContext>())
{
    dbContext.Database.Migrate();
}

//Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();