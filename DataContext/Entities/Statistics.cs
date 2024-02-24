namespace UserDataService.DataContext.Entities
{
    public class Statistics : BaseEntity
    {
        public double WinrateAgainstIA { get; set; }
        public double WinrateAgainstHumanPlayers { get; set; }
        public List<Game> Games { get; set; }
    }
}
