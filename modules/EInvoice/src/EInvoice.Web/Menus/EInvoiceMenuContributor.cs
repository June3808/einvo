using System.Collections.Generic;
using System.Threading.Tasks;
using EInvoice.Localization;
using EInvoice.Permissions;
using Volo.Abp.UI.Navigation;

namespace EInvoice.Web.Menus;

public class EInvoiceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<EInvoiceResource>();
         //Add main menu items.
        var einvoiceMenu = new ApplicationMenuItem(EInvoiceMenus.Prefix, displayName: "EInvoice", "~/EInvoice", icon: "fa fa-globe");

        if (await context.IsGrantedAsync(EInvoicePermissions.InvoiceJournals.Default))
        {
            einvoiceMenu.AddItem(
                new ApplicationMenuItem(EInvoiceMenus.InvoiceJournals, l["Menu:InvoiceJournals"], "~/EInvoice/InvoiceJournals")
            );
        }

        if (!einvoiceMenu.Items.IsNullOrEmpty())
        {
            context.Menu.AddItem(einvoiceMenu);
        }
    }
}
