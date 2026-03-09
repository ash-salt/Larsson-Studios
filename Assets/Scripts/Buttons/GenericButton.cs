using UnityEngine;
using System;
using Assets.Scripts.player_actions;
public class GenericButton : MonoBehaviour
{
    public Texture2D cursor;
    [SerializeField] int actionIndex;
    [SerializeField] public ActionData action;
    [SerializeField] private SpriteRenderer cooldown;
    private Type actionType;
    private ButtonInstruction instructions;
    public SpriteRenderer spriteRenderer;
    private PlayerScript player;
    public void Start()
    {   
        player = GameObject.FindFirstObjectByType<PlayerScript>();
        Refresh();
        player.OnActionsChanged += Refresh;
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked!");
        if (instructions == null)
    {
        Debug.LogError("Instructions is NULL when clicking");
        return;
    }

        instructions.Execute();
        
    }
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        instructions.Update(); //maybe?
    }

    public void SetCooldown(Type act, bool onCooldown)
    {
        Debug.Log("Setting!");
        if (act != actionType) return;
        Debug.Log("Enabling or disabling!");
        Debug.Log(onCooldown);
        cooldown.enabled = onCooldown;
    }
    public void Refresh()
    {

    ActionData act = player.getAction(actionIndex);
    
    if (act == null) {
        Debug.LogError("Action null");
        return; 
    }

    action = act;
    actionType = action.createAction().GetType();
    spriteRenderer.sprite = action.getSprite();
    
    instructions = action.createButtonInstruction();
    if (instructions == null)
    {
        Debug.LogError("Instructions is null at start");
        return;
    }
    instructions.Instruct(this);
}
    
}