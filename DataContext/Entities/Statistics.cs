namespace UserDataService.DataContext.Entities
{
    public class Statistics : BaseEntity
    {
        public double WinrateAI { get; set; }
        public double WinrateFriends { get; set; }
        public int UserId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
