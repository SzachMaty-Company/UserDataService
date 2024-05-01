namespace UserDataService.Models
{
    public class GameDto
    {
        public string White { get; set; } //Nicks
        public string Black { get; set; }
        public string Win { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
        public List<string> Moves { get; set; }
    }
}
