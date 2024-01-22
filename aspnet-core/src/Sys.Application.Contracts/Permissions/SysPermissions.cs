namespace Sys.Permissions;

public static class SysPermissions
{


    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public class AuditLog
    {    
        public const string GroupName = "AuditLog";

        public const string Default = GroupName + ".Default";
    }
}
