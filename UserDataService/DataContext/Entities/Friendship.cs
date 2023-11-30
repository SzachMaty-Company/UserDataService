namespace UserDataService.DataContext.Entities
{
    public class Friendship : BaseEntity
    {
        public int FriendId { get; set; }
        public virtual User Friend { get; set; }
    }
}
