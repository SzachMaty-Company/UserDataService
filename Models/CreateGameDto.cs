namespace UserDataService.Models
{
    public class CreateGameDto
    {
        public string gameCode { get; set; }
        public string whiteUserId { get; set; }
        public string blackUserId { get; set; }
        public string gameMode { get; set; }
        public string gameTime { get; set; }
        public string gameStatus { get; set; }
        public List<string> fenList { get; set; }
        public List<string> moveList { get; set; }
    }
}
