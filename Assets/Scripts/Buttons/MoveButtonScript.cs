using Assets.Scripts.player_actions;
using UnityEngine;

public class MoveButtonScript : ButtonInstruction
{
    public Texture2D cursor;
    private PlayerScript player;
    private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private PositionIndicatorSprite positionIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;
    private MoveAction action;

    public MoveButtonScript(MoveAction action)
    {
        this.action = action;
    }
    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = action.getSprite();
        action.Initialize(Vector2.zero, 0, Vector2.zero);

        player = GameObject.FindFirstObjectByType<PlayerScript>();
        actionUIManager = GameObject.FindFirstObjectByType<ActionUIManager>();

        rangeIndicator = GameObject.FindFirstObjectByType<MovementRangeIndicator>();
        positionIndicator = GameObject.FindFirstObjectByType<PositionIndicatorSprite>();
    }

    public override void Execute()
    {
        Debug.Log("Move button clicked! Click on the board to select target...");
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
        
        Debug.Log("Clicked!");
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
            
            Debug.Log($"Moving to: {validatedTarget}");
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
            action.Initialize(validatedTarget, player.maxMoveDistance, actionUIManager.getUIPosition());
            GameStateManager.Instance.newMove(action, action.getSprite());
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