using Assets.Scripts.player_actions;
using UnityEngine;

public class MoveButtonScript : MonoBehaviour
{
    public Texture2D cursor;
    [SerializeField] private PlayerScript player;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    void OnMouseDown()
    {
        print("Move button clicked! Click on the board to select target...");
        waitingForTarget = true;
        buttonJustClicked = true;
    }

    void Update()
    {
        // Skip the frame where button was clicked
        if (buttonJustClicked)
        {
            buttonJustClicked = false;
            return;
        }

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            
            print($"Moving to: {targetPosition}");
            player.QueueMove(targetPosition, player.maxMoveDistance);
            
            waitingForTarget = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnMouseEnter()
    {
        if (!waitingForTarget)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }
    
    void OnMouseExit()
    {
        if (!waitingForTarget)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}