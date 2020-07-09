using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Identity
{
    public class UserInfo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserInfo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string Id { get; private set; }

        public string UserName { get; private set; }

    }
}
