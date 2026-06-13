using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;

namespace EasySell
{
    public sealed class EasySellSettings : AttributeGlobalSettings<EasySellSettings>
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

        [SettingPropertyBool("Товары")]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Goods { get; set; } = true;

        [SettingPropertyBool("Оружие")]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Weapon { get; set; } = false;

        [SettingPropertyBool("Броня")]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Armor { get; set; } = false;

        [SettingPropertyBool("Еда")]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Food { get; set; } = false;

        [SettingPropertyBool("Лошадей и экипировку лошади")]
        [SettingPropertyGroup("Фильтры продажи")]
        public bool Horse { get; set; } = false;

    }
}
