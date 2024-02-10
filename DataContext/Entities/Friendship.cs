namespace UserDataService.DataContext.Entities
{
    public class Friendship : BaseEntity
    {
        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
