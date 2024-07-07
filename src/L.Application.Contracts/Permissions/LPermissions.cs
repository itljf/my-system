namespace L.Permissions;

public static class LPermissions
{
    public const string GroupName = "L";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Books
    {
        public const string Default = GroupName + ".Books";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Blogs
    {
        public const string Default = GroupName + ".Blogs";
    }
    public static class BlogMgr
    {
        public const string Default = GroupName + ".BlogMgr";
    }
    public static class TagMgr
    {
        public const string Default = GroupName + ".TagMgr";
    }
    public static class GroupMgr
    {
        public const string Default = GroupName + ".GroupMgr";
    }
    public static class NavigationMgr
    {
        public const string Default = GroupName + ".NavigationMgr";
    }
    public static class Nav
    {
        public const string Default = GroupName + ".Nav";
    }
    public static class Settings
    {
        public const string Default = GroupName + ".Settings";
    }
    
    public const string BlogAnalysis = GroupName + ".BlogAnalysis";
    /// <summary>
    /// 系统设置
    /// </summary>
    public static class Setting
    {
        public const string SystemName = GroupName + ".SystemName";
    }

    public const string Todo = GroupName + ".Todo";
}
