namespace UserDataService.Models
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public double WinrateAgainst { get; set; }
        public StatisticsDto Statistics { get; set; }
    }
}
