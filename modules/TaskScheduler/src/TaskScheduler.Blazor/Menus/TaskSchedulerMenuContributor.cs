using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace TaskScheduler.Blazor.Menus;

public class TaskSchedulerMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(TaskSchedulerMenus.Prefix, displayName: "TaskScheduler", "/TaskScheduler", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
