namespace UserDataService.Models
{
    public class GameDto
    {
        public string White { get; set; }
        public string Black { get; set; }
        public string Win { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<MoveDto> History { get; set; }
    }
}
