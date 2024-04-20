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
                    Name = "AI",
                    Email = "AI",
                    Surname = "AI"
                },
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
                },
                new User
                {
                    Name = "Piotr",
                    Email = "piotrnowak@wp.pl",
                    Surname = "Nowak"
                }
            };

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            users = dbContext.Users.ToList();

            /*dbContext.Add(new Friendship
            {
                UserId = users[1].Id,
                FriendId = users[2].Id,
                WinrateAgainst = 40,
                IsAccepted = true,
            });

            dbContext.Add(new Friendship
            {
                UserId = users[2].Id,
                FriendId = users[1].Id,
                WinrateAgainst = 60,
                IsAccepted = true,
            });

            dbContext.Add(new Friendship
            {
                UserId = users[1].Id,
                FriendId = users[3].Id,
                WinrateAgainst = 50,
                IsAccepted = true,
            });
            dbContext.Add(new Friendship
            {
                UserId = users[3].Id,
                FriendId = users[1].Id,
                WinrateAgainst = 50,
                IsAccepted = true,
            });*/

            dbContext.SaveChanges();
            return dbContext;
        }
    }
}
