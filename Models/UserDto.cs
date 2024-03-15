namespace UserDataService.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double WinrateFriends { get; set; }
        public int WinFriends { get; set; }
        public int PlayFriends { get; set; }
        public double WinrateAI { get; set; }
        public int WinAI { get; set; }
        public int PlayAI { get; set; }
        public IEnumerable<FriendDto> FriendList { get; set; }
        public IEnumerable<GameDto> Matches { get; set; }
    }
}
