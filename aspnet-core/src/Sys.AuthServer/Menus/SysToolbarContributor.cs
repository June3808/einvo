using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Users;

namespace Sys.Web.Menus;

public class SysToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        //if (context.Toolbar.Name != StandardToolbars.Main)
        //{
        //    return Task.CompletedTask;
        //}

        context.Toolbar.Items.Clear();

        return Task.CompletedTask;
    }
}
