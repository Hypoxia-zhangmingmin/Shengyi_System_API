namespace Shengyi_WebAPI.Models
{
    public class LoginedUserInfo
    {
        public int Id { get; internal set; } = -1;
        public string Name { get; internal set; } = string.Empty;
        public string Phone { get; internal set; } = string.Empty;

        public DateTime LoginTime { get; internal set; }
        public DateTime ExpiredTime { get; internal set; }
    }
}
