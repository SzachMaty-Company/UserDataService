namespace UserDataService.DataContext.Entities
{
    public class Move : BaseEntity
    {
        public string Code { get; set; } //for example d4, Ne8+
        public int GameId { get; set; }
    }
}
