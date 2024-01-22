namespace EasyAbp.FileManagement
{
    public static class FileManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "DOT_";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "FileManagement";
    }
}
