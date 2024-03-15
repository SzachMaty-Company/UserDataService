namespace UserDataService.DataContext.Entities
{
    public class Move : BaseEntity
    {
        public bool IsWhite { get; set; }
        public string Code { get; set; } //for example d4, Ne8+
        public int GameId { get; set; }
    }
}
