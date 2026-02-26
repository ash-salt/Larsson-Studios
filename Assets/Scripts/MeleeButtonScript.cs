using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;

public class MeleeButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Texture2D cursor;
    [SerializeField] private PlayerScript player;
    [SerializeField] private ActionUIManager actionUIManager;
    [SerializeField] private Sprite actionSprite;
    private SlashDirectionIndicator slashIndicator;
    private float spawnDistance = 0.75f;
    IAction action;

    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    void Start()
    {
        slashIndicator = player.GetComponent<SlashDirectionIndicator>();
    }
    void OnMouseDown()
    {
        waitingForTarget = true;
        buttonJustClicked = true;
        slashIndicator.Show(actionUIManager.GetLastTargetPosition());
        print("Clicked!");
        //actionUIManager.UpdateActionUI(actionSprite);
    }
    void Update()
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
            
            player.EnqueueAction(new MeleeAttack(rotation, spawnPosition));
            waitingForTarget = false;
            slashIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            actionUIManager.UpdateActionUI(actionSprite);
        }
        
    }
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    
   


}
