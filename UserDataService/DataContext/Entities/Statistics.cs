namespace UserDataService.DataContext.Entities
{
    public class Statistics : BaseEntity
    {
        public virtual List<Game> Games { get; set; }
    }
}
