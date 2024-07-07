using L.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace L.Settings;

public class LSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        context.Add(new SettingDefinition(LSettings.SystemName,"ljf的系统",new LocalizableString(
            typeof(LResource),
            LSettings.SystemName
        )));



    }
}
