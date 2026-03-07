using UnityEngine;
namespace Assets.Scripts.player_actions {
public abstract class AAction : ScriptableObject
{
    [SerializeField] protected int cost = 1;
    [SerializeField] public int cooldown = 0;
    [SerializeField] protected Sprite actionSprite;
    protected ButtonInstruction buttonInstruction;
    public int getCost()
    {
        return cost;
    }
    public int getCooldown()
    {
        return cooldown;
    }
    public Sprite getSprite()
    {
        return actionSprite;
    }
    public ButtonInstruction getInstructions()
    {
        return buttonInstruction;
    }
    public abstract void CopyFrom(AAction source);
    public abstract void execute(EntityScript target);
}

}