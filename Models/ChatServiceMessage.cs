namespace UserDataService.Models
{
    public class ChatMember
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class ChatServiceMessage
    {
        public List<ChatMember> ChatMembers = [];
    }
}
