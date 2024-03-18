namespace EInvoice.Settings;

public static class EInvoiceSettings
{
    public const string GroupName = "EInvoice";

    /* Add constants for setting names. Example:
     * public const string MySettingName = GroupName + ".MySettingName";
     */

    public class EInvoice
    {
        public const string Default = GroupName + ".IRBM";
        public const string URLAddress = Default + ".URLAddress";
    }
}
