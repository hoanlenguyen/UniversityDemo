namespace UniversityDemo.Permissions
{
    public static class RequiredPermissions
    {
        public static class Accounts
        {
            public const string View = "Permissions.Accounts.View";
            public const string Create = "Permissions.Accounts.Create";
            public const string Edit = "Permissions.Accounts.Edit";
            public const string Delete = "Permissions.Accounts.Delete";
        }

        public static class Blogs
        {
            public const string View = "Permissions.Blogs.View";
            public const string Create = "Permissions.Blogs.Create";
            public const string Edit = "Permissions.Blogs.Edit";
            public const string Delete = "Permissions.Blogs.Delete";
        }
    }
}