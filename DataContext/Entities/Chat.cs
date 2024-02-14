namespace UserDataService.DataContext.Entities
{
    public class Chat : BaseEntity
    {
        public int GameId { get; set; }
        public List<Message> Messages { get; set; }
    }
}
