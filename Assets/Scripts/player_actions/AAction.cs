using UnityEngine;
namespace Assets.Scripts.player_actions {
public abstract class AAction
{
    public ActionData actionData;
    public EntityScript target;
    public int getCost()
    {
        return actionData.cost;
    }
    public int getCooldown()
    {
        return actionData.cooldown;
    }
    public Sprite getSprite()
    {
        return actionData.actionSprite;
    }
    /*
    public ButtonInstruction getInstructions()
    {
            if (buttonInstruction is null)
            {
                buttonInstruction = actionData.createButtonInstruction();
            }
            return buttonInstruction;
    }*/
    public abstract void execute();
}

}