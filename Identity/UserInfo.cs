using System.Collections.Generic;

namespace UniversityDemo.Identity
{
    public interface IUserInfo
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}