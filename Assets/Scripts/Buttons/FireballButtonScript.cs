using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;
public class FireballButtonScript : ButtonInstruction
{
    private FireballActionData actionData;
    private PlayerScript player;
    private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;
    private float maxRange;

    public FireballButtonScript(FireballActionData actionData)
    {
        this.actionData = actionData;
        this.maxRange = actionData.maxRange;
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
            rangeIndicator.Show(startPos, maxRange);
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

            float colliderRadius = 0.1f;

            Vector2 validatedTarget = MovementUtility.FindNearestValidPosition(
                actionUIManager.GetLastTargetPosition(),
                targetPosition,
                maxRange,
                colliderRadius
            );

            waitingForTarget = false;
            if (rangeIndicator != null) { 
                rangeIndicator.Hide();
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            FireballAction action = (FireballAction) actionData.createAction();
            action.Initialize(validatedTarget);
            GameStateManager.Instance.newAction(action);
        }
    }
}