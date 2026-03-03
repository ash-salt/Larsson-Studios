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
        
        Vector2 startPos = actionUIManager.GetLastTargetPosition();
        
        if (rangeIndicator != null)
        {
            rangeIndicator.Show(startPos);
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
            

            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            
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
            
            Vector2 validatedTarget = MovementUtility.FindNearestValidPosition(
                actionUIManager.GetLastTargetPosition(),
                targetPosition,
                player.maxMoveDistance,
                colliderRadius
            );
            
            print($"Moving to: {validatedTarget}");
            waitingForTarget = false;
            
            if (rangeIndicator != null)
            {
                rangeIndicator.Hide();
            }
            

            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            GameStateManager.Instance.newMove(new MoveAction(validatedTarget, player.maxMoveDistance, actionUIManager.getUIPosition()), actionSprite);
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