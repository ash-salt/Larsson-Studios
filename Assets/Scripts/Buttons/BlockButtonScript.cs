using Assets.Scripts.player_actions;
//using System.Diagnostics;
using UnityEngine;

public class BlockButtonScript : ButtonInstruction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Texture2D cursor;
    BlockAction action;

    public BlockButtonScript(BlockAction action)
    {
        this.action = action;
    }

    public override void Update()
    {
        return;
    }
    public override void Execute()
    {
        Debug.Log("Clicked!");
        GameStateManager.Instance.newAction(action, action.getSprite());
    }    

    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = action.getSprite();
    }

}
