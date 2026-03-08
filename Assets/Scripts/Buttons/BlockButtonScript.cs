using Assets.Scripts.player_actions;
//using System.Diagnostics;
using UnityEngine;
public class BlockButtonScript : ButtonInstruction
{
    private BlockActionData actionData;

    public BlockButtonScript(BlockActionData actionData)
    {
        this.actionData = actionData;
    }

    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = actionData.getSprite();
    }

    public override void Execute()
    {
        AAction blockAction = actionData.createAction();
        GameStateManager.Instance.newAction(blockAction);
    }
    public override void Update() { }
}