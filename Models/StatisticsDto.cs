namespace UserDataService.Models
{
    public class StatisticsDto
    {
        public double WinrateAI { get; set; }
        public int PlayAI { get; set; }
        public int WinAI { get; set; }
        public double WinrateFriends { get; set; }
        public int PlayFriends { get; set; }
        public int WinFriends { get; set; }
        public List<GameDto> Games { get; set; } = null;
    }
}
