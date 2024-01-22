using Volo.Abp.Settings;

namespace Sys.Settings;

public class SysSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SysSettings.MySetting1));
    }
}
