namespace UserDataService.DataContext.Entities
{
    public class User : BaseEntity
    {
        public Statistics Statistics { get; set; }
        public List<Friendship> Friendships { get; set; }
    }
}
