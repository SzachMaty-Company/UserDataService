
namespace UserDataService.DataContext.Entities
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
