using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace EasySell.Settings
{
    public sealed class ModSettings : AttributeGlobalSettings<ModSettings>
    {
        public override string Id => "EasySellMod";
        public override string DisplayName => "Easy Sell Mod";
        public override string FolderName => "EasySellMod";
        public override string FormatType => "json";


        [SettingPropertyInteger(
                "Цена продажи",
                0,
                100000,
                Order = 1,
                RequireRestart = false,
                HintText = "Продавайте товары по цене ниже этой стоимости."),]
        [SettingPropertyGroup("Основное", GroupOrder = 1)]

        public int PriceThreshold { get; set; } = 1000;

        [SettingPropertyBool("Товары", Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Goods { get; set; } = true;

        [SettingPropertyBool("Оружие", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Weapon { get; set; } = false;

        [SettingPropertyBool("Броня", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Armor { get; set; } = false;

        [SettingPropertyBool("Еда", Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Food { get; set; } = false;

        [SettingPropertyBool("Лошади и экипировку лошади", Order = 0, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Horse { get; set; } = false;

    }
}
