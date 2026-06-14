using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Localization;

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
        "{=ES_ZimwanKmqQ}Sell Price",
        0,
        100000,
        Order = 3,
        RequireRestart = false,
        HintText = "{=ES_Ihp6skXIwe}Sell items below this price"
    )]
    [SettingPropertyGroup("{=ES_Wzfkoth5qJ}General", GroupOrder = 2)]
    public int PriceThreshold { get; set; } = 1000;

    [SettingPropertyBool(
        "{=ES_5Kjiok81rR}Food Protection",
        Order = 2,
        RequireRestart = false,
        HintText = "{=ES_bLvvOFIgpk}Keep food amount equal to the food reserve"
    )]
    [SettingPropertyGroup("{=ES_Wzfkoth5qJ}General", GroupOrder = 2)]
    public bool IsFoodProtection { get; set; } = true;

    [SettingPropertyInteger(
        "{=ES_9xXCnZVW2R}Food Reserve",
        0,
        100,
        Order = 1,
        RequireRestart = false
    )]
    [SettingPropertyGroup("{=ES_Wzfkoth5qJ}General", GroupOrder = 2)]
    public int FoodReserveAmount { get; set; } = 10;

    // =========================================================
    // ГРУППА 2: Фильтры продажи
    // =========================================================
    [SettingPropertyBool(
        "{=ES_oppcBiajIG}Horses and Horse Equipment",
        Order = 1,
        RequireRestart = false
    )]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Horse { get; set; } = false;

    [SettingPropertyBool("{=ES_0wG4eY5VJt}Goods", Order = 2, RequireRestart = false)]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Goods { get; set; } = true;

    [SettingPropertyBool("{=ES_PX2UqtZL09}Weapons", Order = 3, RequireRestart = false)]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Weapon { get; set; } = false;

    [SettingPropertyBool("{=ES_Ksd2dTmj67}Armor", Order = 4, RequireRestart = false)]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Armor { get; set; } = false;

    [SettingPropertyBool("{=ES_Ly9sUUSUiD}Food", Order = 5, RequireRestart = false)]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Food { get; set; } = false;

    [SettingPropertyBool("{=ES_GCUn5kTMHd}Animals", Order = 6, RequireRestart = false)]
    [SettingPropertyGroup("{=ES_gg2siUr5Lu}Filters", GroupOrder = 1)]
    public bool Animal { get; set; } = false;
}
