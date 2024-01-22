using Volo.Abp.Reflection;

namespace TaskScheduler.Permissions;

public class TaskSchedulerPermissions
{
    public const string GroupName = "TaskScheduler";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TaskSchedulerPermissions));
    }

    public class TaskScheduler
    {
        public const string Default = GroupName + ".TaskScheduler";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
