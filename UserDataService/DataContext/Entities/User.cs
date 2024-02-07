namespace UserDataService.DataContext.Entities
{
    public class User : BaseEntity
    {
        public virtual Statistics Statistics { get; set; }
        public virtual List<Friendship> Friendships { get; set; }
    }
}
