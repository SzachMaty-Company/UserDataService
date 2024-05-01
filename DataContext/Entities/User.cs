namespace UserDataService.DataContext.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Statistics Statistics { get; set; } = new Statistics();
        public List<Friendship> Friendships { get; set; } = new List<Friendship>();
    }
}
