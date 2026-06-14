using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using EasySell.Settings;

namespace EasySell.Behaviors
{
    public class SellBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(
                this,
                OnSessionLaunched);
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            starter.AddGameMenuOption(
                "town",
                "auto_sell_items",
                "Продать дешёвые вещи",
                SellItemsCondition,
                SellItemsConsequence,
                false,
                5);
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private bool SellItemsCondition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Trade;
            return Settlement.CurrentSettlement?.Town != null;
        }

        private void SellItemsConsequence(MenuCallbackArgs args)
        {
            var modSetting = ModSettings.Instance;
            var settlement = Settlement.CurrentSettlement;

            if (modSetting == null)
                return;

            var mainParty = MobileParty.MainParty;

            var itemRoster = mainParty.ItemRoster;

            if (itemRoster.Count < 1)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage("У вас нет вещей для продажи."));
                return;
            }

            int totalGold = 0;
            int soldCount = 0;

            for (int i = itemRoster.Count - 1; i >= 0; i--)
            {

                var rosterElement = itemRoster[i];

                if (rosterElement.Amount <= 0)
                    continue;

                var itemObject = itemRoster.GetItemAtIndex(i);
                int countToSell = rosterElement.Amount;


                if (modSetting.IsFoodProtection && itemObject.IsFood)
                {
                    countToSell = rosterElement.Amount > modSetting.FoodReserveAmount ? rosterElement.Amount - modSetting.FoodReserveAmount : 0;
                }

                if (countToSell <= 0)
                    continue;

                if (!CanSell(itemObject))
                    continue;

                int itemPrice = settlement.Town.GetItemPrice(
                    itemObject,
                    mainParty,
                    true);

                if (itemPrice <= 0 || itemPrice > modSetting.PriceThreshold)
                    continue;

                totalGold += itemPrice * countToSell;
                soldCount += countToSell;
                itemRoster.AddToCounts(rosterElement.EquipmentElement, -countToSell);
            }

            if (totalGold == 0)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage(
                        $"Нет подходящих предметов для продажи"));
                return;
            }

            GiveGoldAction.ApplyBetweenCharacters(
                null,
                Hero.MainHero,
                totalGold);



            InformationManager.DisplayMessage(
                new InformationMessage(
                    $"Продано предметов: {soldCount}. Получено: {totalGold} денаров."));
        }

        private static bool CanSell(ItemObject item)
        {
            var settings = ModSettings.Instance;

            if (settings == null)
                return false;

            if (item.IsUniqueItem || item.NotMerchandise)
                return false;

            if (item.IsFood)
                return settings.Food;

            switch (item.ItemType)
            {
                case ItemObject.ItemTypeEnum.Horse:
                case ItemObject.ItemTypeEnum.HorseHarness:
                    return settings.Horse;

                case ItemObject.ItemTypeEnum.Animal:
                    return settings.Animal;

                case ItemObject.ItemTypeEnum.OneHandedWeapon:
                case ItemObject.ItemTypeEnum.TwoHandedWeapon:
                case ItemObject.ItemTypeEnum.Polearm:
                case ItemObject.ItemTypeEnum.Bow:
                case ItemObject.ItemTypeEnum.Crossbow:
                case ItemObject.ItemTypeEnum.Thrown:
                    return settings.Weapon;

                case ItemObject.ItemTypeEnum.HeadArmor:
                case ItemObject.ItemTypeEnum.BodyArmor:
                case ItemObject.ItemTypeEnum.LegArmor:
                case ItemObject.ItemTypeEnum.HandArmor:
                case ItemObject.ItemTypeEnum.Cape:
                    return settings.Armor;
            }

            return settings.Goods && item.IsTradeGood;
        }
    }
}
