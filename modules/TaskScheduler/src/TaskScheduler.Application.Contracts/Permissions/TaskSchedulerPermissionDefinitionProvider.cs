using TaskScheduler.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TaskScheduler.Permissions;

public class TaskSchedulerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TaskSchedulerPermissions.GroupName, L("Permission:TaskScheduler"));

        var taskSchedulerPermission = myGroup.AddPermission(TaskSchedulerPermissions.TaskScheduler.Default, L("Permission:InvoiceJournals"));
        taskSchedulerPermission.AddChild(TaskSchedulerPermissions.TaskScheduler.Create, L("Permission:Create"));
        taskSchedulerPermission.AddChild(TaskSchedulerPermissions.TaskScheduler.Update, L("Permission:Update"));
        taskSchedulerPermission.AddChild(TaskSchedulerPermissions.TaskScheduler.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TaskSchedulerResource>(name);
    }
}
