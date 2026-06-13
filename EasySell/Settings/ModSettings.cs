using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace EasySell.Settings;

public sealed class ModSettings : AttributeGlobalSettings<ModSettings>
{
        public override string Id => "EasySellMod";
        public override string DisplayName => "Easy Sell Mod";
        public override string FolderName => "EasySellMod";
        public override string FormatType => "json";

        // =========================================================
        // ГРУППА 1: Основное
        // =========================================================

        [SettingPropertyInteger(
            "Цена продажи",
            0,
            100000,
            Order = 3,
            RequireRestart = false,
            HintText = "Продавайте товары по цене ниже этой стоимости")]
        [SettingPropertyGroup("Основное", GroupOrder = 2)]
        public int PriceThreshold { get; set; } = 1000;

        [SettingPropertyBool(
            "Защита продовольствия",
            Order = 2,
            RequireRestart = false, HintText = "Оставлять количество еды в размере продовольственного резерва")]
        [SettingPropertyGroup("Основное", GroupOrder = 2)]
        public bool IsFoodProtection { get; set; } = true;

        [SettingPropertyInteger(
            "Продовольственный резерв",
            0,
            100,
            Order = 1,
            RequireRestart = false)]
        [SettingPropertyGroup("Основное", GroupOrder = 2)]
        public int FoodReserveAmount { get; set; } = 10;

        // =========================================================
        // ГРУППА 2: Фильтры продажи
        // =========================================================
        [SettingPropertyBool("Лошади и экипировка лошади", Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Horse { get; set; } = false;

        [SettingPropertyBool("Товары", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Goods { get; set; } = true;

        [SettingPropertyBool("Оружие", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Weapon { get; set; } = false;

        [SettingPropertyBool("Броня", Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Armor { get; set; } = false;

        [SettingPropertyBool("Еда", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Food { get; set; } = false;

        [SettingPropertyBool("Животные", Order = 6, RequireRestart = false)]
        [SettingPropertyGroup("Фильтры", GroupOrder = 1)]
        public bool Animal { get; set; } = false;







}