namespace UserDataService.DataContext.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Statistics Statistics { get; set; }
        public List<Friendship> Friendships { get; set; }
    }
}
