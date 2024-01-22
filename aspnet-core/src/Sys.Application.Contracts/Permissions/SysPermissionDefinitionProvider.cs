using Sys.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Sys.Permissions;

public class SysPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(SysPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SysPermissions.MyPermission1, L("Permission:MyPermission1"));

        var AuditLog = context.AddGroup(SysPermissions.AuditLog.GroupName);

        AuditLog.AddPermission(SysPermissions.AuditLog.Default,
            L("Menu:LoggingManagement"), MultiTenancySides.Host);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SysResource>(name);
    }
}
