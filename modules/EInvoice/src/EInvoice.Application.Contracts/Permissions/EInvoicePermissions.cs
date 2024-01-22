using Volo.Abp.Reflection;

namespace EInvoice.Permissions;

public class EInvoicePermissions
{
    public const string GroupName = "EInvoice";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(EInvoicePermissions));
    }
    public class InvoiceJournals
    {
        public const string Default = GroupName + ".InvoiceJournals";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
