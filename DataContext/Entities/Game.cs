namespace UserDataService.DataContext.Entities
{
    public class Game : BaseEntity
    {
        public List<Move> Moves { get; set; }
    }
}
