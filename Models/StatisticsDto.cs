namespace UserDataService.Models
{
    public class StatisticsDto
    {
        public double WinrateIA { get; set; }
        public double WinrateFriends { get; set; }
        public List<GameDto> Games { get; set; }
    }
}
