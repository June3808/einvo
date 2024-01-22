using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sys.Localization;
using Sys.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Sys.Permissions;

namespace Sys.Web.Menus;

public class SysMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public SysMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<SysResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                SysMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);


        //var loggingManagementMenuItem = new ApplicationMenuItem("AuditLogs", l["Menu:LoggingManagement"], icon: "fa fa-file-medical-alt");

        if (await context.IsGrantedAsync(SysPermissions.AuditLog.Default))
        {
            context.Menu.GetAdministration().Items.Add(new ApplicationMenuItem(SysMenus.AuditLogs, l["Menu:LoggingManagement"], "/AuditLogs", icon: "fa fa-file-medical-alt"));
        }

    }

    private async Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SysResource>();
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();
        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}", icon: "fa fa-cog", order: 1000, null, "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000).RequireAuthenticated());



        //insert custom menu here
        //var loggingManagementMenuItem = new ApplicationMenuItem("AuditLogs", l["Menu:LoggingManagement"], icon: "fa fa-file-medical-alt");

        ////if (await context.IsGrantedAsync(SysPermissions.AuditLog.Default))
        ////{
        //    loggingManagementMenuItem.AddItem(
        //        new ApplicationMenuItem(SysMenus.AuditLogs, l["Menu:LoggingManagement"], "/AuditLogs")
        //    );
        ////}

        //if (loggingManagementMenuItem.Items.Count > 0)
        //{
        //    //context.Menu.GetAdministration().Items.Add(loggingManagementMenuItem);
        //context.Menu.AddItem(new ApplicationMenuItem(SysMenus.AuditLogs, l["Menu:LoggingManagement"], "/AuditLogs"));
        //}
    }
}
