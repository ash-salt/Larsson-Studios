using Assets.Scripts.player_actions;
using UnityEngine;

public class MoveButtonScript : MonoBehaviour
{
    public Texture2D cursor;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Sprite actionSprite;
    [SerializeField] private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    void Start()
    {
        rangeIndicator = player.GetComponent<MovementRangeIndicator>();
    }

    void OnMouseDown()
    {
        print("Move button clicked! Click on the board to select target...");
        waitingForTarget = true;
        buttonJustClicked = true;
        rangeIndicator.Show(actionUIManager.GetLastTargetPosition());
        print("Clicked!");
    }

    void Update()
    {
        if (buttonJustClicked)
        {
            buttonJustClicked = false;
            return;
        }
        

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            
            // Validate the movement using MovementUtility
            Vector2 validatedTarget = MovementUtility.ValidateMovement(
                actionUIManager.GetLastTargetPosition(),
                targetPosition,
                player.maxMoveDistance
            );
            
            print($"Moving to: {validatedTarget}");
            player.QueueMove(targetPosition, player.maxMoveDistance);
            actionUIManager.newMove(validatedTarget); // Store validated position
            waitingForTarget = false;
            rangeIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            actionUIManager.UpdateActionUI(actionSprite);
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