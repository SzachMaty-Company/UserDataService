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
                    Name = "Zbyszek",
                    Email = "zbyszek@o2.pl",
                    Surname = "Ziobro"
                },
                new User
                {
                    Name = "Janek",
                    Email = "kowal@onet.pl",
                    Surname = "Kowalski"
                },
                new User
                {
                    Name = "Grzesiu",
                    Email = "florida@tlen.pl",
                    Surname = "Floryda"
                }
            };
            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
            return dbContext;
        }
    }
}
