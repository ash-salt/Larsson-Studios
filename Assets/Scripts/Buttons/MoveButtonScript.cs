using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;
public class MoveButtonScript : ButtonInstruction
{
    private MoveActionData actionData;
    private PlayerScript player;
    private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    public MoveButtonScript(MoveActionData actionData)
    {
        this.actionData = actionData;
    }

    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = actionData.getSprite();
        player = GameObject.FindFirstObjectByType<PlayerScript>();
        actionUIManager = GameObject.FindFirstObjectByType<ActionUIManager>();
        rangeIndicator = GameObject.FindFirstObjectByType<MovementRangeIndicator>();
    }

    public override void Execute()
    {
        waitingForTarget = true;
        buttonJustClicked = true;
        Vector2 startPos = actionUIManager.GetLastTargetPosition();
        if (rangeIndicator != null) {
            rangeIndicator.Show(startPos);
        }
    }

    public override void Update()
    {
        if (buttonJustClicked) { buttonJustClicked = false; return; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            waitingForTarget = false;
            if (rangeIndicator != null) rangeIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            float colliderRadius = 0.3f;
            CircleCollider2D circleCollider = player.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
                colliderRadius = circleCollider.radius;
            else
            {
                BoxCollider2D boxCollider = player.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                    colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
            }

            Vector2 validatedTarget = MovementUtility.FindNearestValidPosition(
                actionUIManager.GetLastTargetPosition(),
                targetPosition,
                player.maxMoveDistance,
                colliderRadius
            );

            waitingForTarget = false;
            if (rangeIndicator != null) { 
                rangeIndicator.Hide();
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            MoveAction action = (MoveAction)actionData.createAction();
            action.Initialize(validatedTarget, player.maxMoveDistance, actionUIManager.getUIPosition());
            GameStateManager.Instance.newMove(action);
        }
    }
}