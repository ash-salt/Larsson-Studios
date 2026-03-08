using Assets.Scripts.player_actions;
using UnityEngine;

public class EmptyAction : AAction
{
    public int getCost()
    {
        return 1;
    }

    public int getCooldown()
    {
        return 0;
    }

    public override void CopyFrom(AAction source)
    {
        return;
    }

    public override void execute(EntityScript goblin)
    {
        return;
    }
}