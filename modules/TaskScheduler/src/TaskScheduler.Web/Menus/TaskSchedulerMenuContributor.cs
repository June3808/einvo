using System.Threading.Tasks;
using TaskScheduler.Localization;
using TaskScheduler.Permissions;
using Volo.Abp.UI.Navigation;

namespace TaskScheduler.Web.Menus;

public class TaskSchedulerMenuContributor : IMenuContributor
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
        var l = context.GetLocalizer<TaskSchedulerResource>();

        //Add main menu items.
        if (await context.IsGrantedAsync(TaskSchedulerPermissions.TaskScheduler.Default))
        {
            context.Menu.AddItem(new ApplicationMenuItem(TaskSchedulerMenus.Prefix, displayName: l["Menu:TaskScheduler"], "~/TaskScheduler", icon: "fa fa-globe"));
        }
    }
}
