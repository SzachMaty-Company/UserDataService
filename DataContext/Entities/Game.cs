namespace UserDataService.DataContext.Entities
{
    public class Game : BaseEntity
    {
        public virtual List<Move> Moves { get; set; }
    }
}
