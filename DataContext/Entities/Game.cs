namespace UserDataService.DataContext.Entities
{
    public class Game : BaseEntity
    {
        public int WhiteId { get; set; }
        public int BlackId { get; set; }
        public User White { get; set; }
        public User Black { get; set; }
        public string Win { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
        public List<string> Moves { get; set; } = new List<string>();
        //Other game data
    }
}
