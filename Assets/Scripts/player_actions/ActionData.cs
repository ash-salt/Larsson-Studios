using Assets.Scripts.player_actions;
using UnityEngine;

public abstract class ActionData : ScriptableObject
{
    [SerializeField] public int cost = 1;
    [SerializeField] public int cooldown = 0;
    [SerializeField] public Sprite actionSprite;
    [SerializeField] public ButtonInstruction buttonInstruction;

    public abstract AAction createAction();
    public abstract ButtonInstruction createButtonInstruction();

    public Sprite getSprite()
    {
        return actionSprite;
    }
}