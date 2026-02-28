using Assets.Scripts.player_actions;
using UnityEngine;

public class MoveButtonScript : MonoBehaviour
{
    public Texture2D cursor;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Sprite actionSprite;
    [SerializeField] private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private PositionIndicatorSprite positionIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    void Start()
    {
        rangeIndicator = player.GetComponent<MovementRangeIndicator>();
        positionIndicator = player.GetComponent<PositionIndicatorSprite>();
    }

    void OnMouseDown()
    {
        print("Move button clicked! Click on the board to select target...");
        waitingForTarget = true;
        buttonJustClicked = true;
        
        Vector2 startPos = actionUIManager.GetLastTargetPosition();
        
        if (rangeIndicator != null)
        {
            rangeIndicator.Show(startPos);
        }
        
        if (positionIndicator != null)
        {
            positionIndicator.Show(startPos);
        }
        
        print("Clicked!");
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
            
            if (rangeIndicator != null)
            {
                rangeIndicator.Hide();
            }
            
            if (positionIndicator != null)
            {
                positionIndicator.Hide();
            }
            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            
            // Get collider size for validation
            float colliderRadius = 0.3f;
            CircleCollider2D circleCollider = player.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                colliderRadius = circleCollider.radius;
            }
            else
            {
                BoxCollider2D boxCollider = player.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
                }
            }
            
            // Find nearest valid position (snap to valid location)
            Vector2 validatedTarget = MovementUtility.FindNearestValidPosition(
                actionUIManager.GetLastTargetPosition(),
                targetPosition,
                player.maxMoveDistance,
                colliderRadius
            );
            
            print($"Moving to: {validatedTarget}");
            player.QueueMove(validatedTarget, player.maxMoveDistance);
            actionUIManager.newMove(validatedTarget);
            waitingForTarget = false;
            
            if (rangeIndicator != null)
            {
                rangeIndicator.Hide();
            }
            
            if (positionIndicator != null)
            {
                positionIndicator.Hide();
            }
            
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