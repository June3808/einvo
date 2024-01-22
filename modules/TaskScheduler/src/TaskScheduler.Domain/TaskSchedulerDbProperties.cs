namespace TaskScheduler;

public static class TaskSchedulerDbProperties
{
    public static string DbTablePrefix { get; set; } = "DOT_";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "TaskScheduler";
}
