namespace UserDataService.DataContext.Entities
{
    public class Friendship : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public int FriendId { get; set; }
        public User Friend { get; set; } = new User();
        public bool IsAccepted { get; set; }
    }
}
