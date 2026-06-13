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
            var settlement = Settlement.CurrentSettlement;

            if (settlement.Town == null)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage("Автопродажа доступна только в городе."));
                return;
            }

            var itemRoster = MobileParty.MainParty.ItemRoster;

            if (itemRoster.Count < 1)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage("У вас нет вещей для продажи."));
                return;
            }

            int threshold = ModSettings.Instance?.PriceThreshold ?? 1000;
            int totalGold = 0;
            int soldCount = 0;

            for (int i = itemRoster.Count - 1; i >= 0; i--)
            {
                var item = itemRoster[i];
                var itemObject = itemRoster.GetItemAtIndex(i);

                if (item.Amount <= 0)
                    continue;

                if (!CanSell(itemObject))
                    continue;

                int itemPrice = settlement.Town.GetItemPrice(
                    itemObject,
                    MobileParty.MainParty,
                    true);

                if (itemPrice <= 0 || itemPrice > threshold)
                    continue;

                int count = item.Amount;

                totalGold += itemPrice * count;
                soldCount += count;
                itemRoster.AddToCounts(item.EquipmentElement, -count);
            }

            if (totalGold == 0)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage(
                        $"Нет вещей для продажи подходящим условиям"));
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
