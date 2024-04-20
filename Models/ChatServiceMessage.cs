namespace UserDataService.Models
{
    public class ChatMember
    {
        public int userId { get; set; }
        public string username { get; set; }
    }

    public class ChatServiceMessage
    {
        public List<ChatMember> chatMembers = [];
    }
}
