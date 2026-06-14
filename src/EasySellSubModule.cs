using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using EasySell.Behaviors;

namespace EasySell;

public class EasySellSubModule : MBSubModuleBase
{
    protected override void OnGameStart(Game game, IGameStarter starterObject)
    {
        base.OnGameStart(game, starterObject);

        if (game.GameType is Campaign)
        {
            CampaignGameStarter starter =
                (CampaignGameStarter)starterObject;

            starter.AddBehavior(new SellBehavior());
        }
    }

    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
    }

    protected override void OnSubModuleUnloaded()
    {
        base.OnSubModuleUnloaded();
    }

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
        base.OnBeforeInitialModuleScreenSetAsRoot();
    }
}
