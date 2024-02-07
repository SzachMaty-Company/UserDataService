using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserDataService.DataContext.Entities;

namespace UserDataService.DataContext
{
    public class UserDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Statistics> Statistics { get; set; }

        public DbSet<Move> Moves { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
