using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;
public class MissileScript : ButtonInstruction
{
    private MagicMissileData actionData;
    private PlayerScript player;
    private ActionUIManager actionUIManager;
    private MovementRangeIndicator rangeIndicator;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;
    private float maxRange;

    public MissileScript(MagicMissileData actionData)
    {
        this.actionData = actionData;
        maxRange = actionData.maxRange;
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
            rangeIndicator.Show(startPos, maxRange, 0.01f);
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

            waitingForTarget = false;
            if (rangeIndicator != null) { 
                rangeIndicator.Hide();
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            MagicMissile action = (MagicMissile) actionData.createAction();
            action.Initialize(targetPosition);
            GameStateManager.Instance.newAction(action);
            GameStateManager.Instance.Indicate(targetPosition, ((MagicMissileData) actionData).indicator);
        }
    }
}