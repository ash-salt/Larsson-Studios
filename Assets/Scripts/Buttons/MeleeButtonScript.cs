using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;


public class MeleeButtonScript : ButtonInstruction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private ActionUIManager actionUIManager;   
    private PlayerScript player;
    public Texture2D cursor;
    private SlashDirectionIndicator slashIndicator;
    private float spawnDistance = 0.75f;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;
    private MeleeAttack action;

    public MeleeButtonScript(MeleeAttack action)
    {
        this.action = action;
    }
    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = action.getSprite();

        player = GameObject.FindFirstObjectByType<PlayerScript>();
        slashIndicator = player.GetComponent<SlashDirectionIndicator>();

        actionUIManager = GameObject.FindFirstObjectByType<ActionUIManager>();
    }
    public override void Execute()
    {
        UnityEngine.Debug.Log("Clacked!");
        waitingForTarget = true;
        buttonJustClicked = true;
        slashIndicator.Show(actionUIManager.GetLastTargetPosition());
        
    }
    public override void Update()
    {
        if (buttonJustClicked)
        {
            buttonJustClicked = false;
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            waitingForTarget = false;
            slashIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            
            // Validate the movement using MovementUtility
            Vector2 direction = (targetPosition - actionUIManager.GetLastTargetPosition()).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 spawnPosition = actionUIManager.GetLastTargetPosition() + direction * spawnDistance;
            
            
            waitingForTarget = false;
            slashIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            action.Initialize(rotation, spawnPosition);
            GameStateManager.Instance.newAction(action, action.getSprite());
        }
    }
}
