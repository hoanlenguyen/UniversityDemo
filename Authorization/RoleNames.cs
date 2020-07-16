﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Authorization
{
    public static class RoleNames
    {
        public const string Admin = "Administrator";
        public const string Manager = "Manager";
        public const string Member = "Member";
        public const string SuperAdmin = Admin+", "+Manager+", "+ Member;
    }
}
