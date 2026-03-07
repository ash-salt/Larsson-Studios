using UnityEngine;
using System;
using Assets.Scripts.player_actions;
public class GenericButton : MonoBehaviour
{
    public Texture2D cursor;
    [SerializeField] int actionIndex;
    [SerializeField] public AAction action;
    [SerializeField] private SpriteRenderer cooldown;
    private ButtonInstruction instructions;
    public SpriteRenderer spriteRenderer;

    private void ChangeAction(AAction newAction)
    {
        action = newAction;
        spriteRenderer.sprite = action.getSprite();
        instructions = action.getInstructions();
        instructions.Instruct(this);
    }
    public void Start()
    {   
        PlayerScript player = GameObject.FindFirstObjectByType<PlayerScript>();
        AAction act = player.getAction(actionIndex);
        if (act != null)        {
            action = act;
        }
        
        instructions = action.getInstructions();
        if (instructions == null)
        {
            Debug.LogError("Instructions is NULL at start");
            return;
        }
        instructions.Instruct(this);
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
        if (act != action.GetType()) return;
        Debug.Log("Enabling or disabling!");
        Debug.Log(onCooldown);
        cooldown.enabled = onCooldown;
    }
    
}