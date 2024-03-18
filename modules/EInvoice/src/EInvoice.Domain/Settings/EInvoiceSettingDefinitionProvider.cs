using EInvoice.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EInvoice.Settings;

public class EInvoiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        /* Define module settings here.
         * Use names from EInvoiceSettings class.
         */

        context.Add(
         new SettingDefinition(
             EInvoiceSettings.EInvoice.URLAddress,
             "",
             L("IRBMAPIURL")
         //L("FilemanagementSFTPURLDesc")
         )
         .WithProperty("Group1", "IRBM"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EInvoiceResource>(name);
    }
}
