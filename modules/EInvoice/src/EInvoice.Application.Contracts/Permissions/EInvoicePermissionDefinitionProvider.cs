using EInvoice.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EInvoice.Permissions;

public class EInvoicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EInvoicePermissions.GroupName, L("Permission:EInvoice"));

        var invoiceJournalsPermission = myGroup.AddPermission(EInvoicePermissions.InvoiceJournals.Default, L("Permission:InvoiceJournals"));
        invoiceJournalsPermission.AddChild(EInvoicePermissions.InvoiceJournals.Create, L("Permission:Create"));
        invoiceJournalsPermission.AddChild(EInvoicePermissions.InvoiceJournals.Update, L("Permission:Update"));
        invoiceJournalsPermission.AddChild(EInvoicePermissions.InvoiceJournals.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EInvoiceResource>(name);
    }
}
