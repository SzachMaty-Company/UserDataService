namespace UserDataService.DataContext.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Statistics Statistics { get; set; }
        public List<Friendship> Friendships { get; set; }
        public List<Game> Games { get; set; }
    }
}
