namespace UserDataService.DataContext.Entities
{
    public class Friendship : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int FriendId { get; set; }
        public User Friend { get; set; }
        public double WinrateAgainst { get; set; }
        public bool IsAccepted { get; set; }
        public int SentBy { get; set; }
    }
}
