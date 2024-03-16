using UserDataService.DataContext;
using UserDataService.DataContext.Entities;

namespace UserDataService
{
    public static class Seeder
    {
        public static UserDataContext Seed(this UserDataContext dbContext)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Zbyszek",
                    Email = "zbyszek@o2.pl",
                    Surname = "Ziobro"
                },
                new User
                {
                    Id = 2,
                    Name = "Janek",
                    Email = "kowal@onet.pl",
                    Surname = "Kowalski"
                },
                new User
                {
                    Id = 3,
                    Name = "Grzesiu",
                    Email = "florida@tlen.pl",
                    Surname = "Floryda"
                }
            };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();
            return dbContext;
        }
    }
}
