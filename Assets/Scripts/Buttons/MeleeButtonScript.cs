using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;

public class MeleeButtonScript : ButtonInstruction
{
    private ActionUIManager actionUIManager;
    private PlayerScript player;
    private SlashDirectionIndicator slashIndicator;
    private MeleeAttackData actionData;
    private float spawnDistance = 0.75f;
    private bool waitingForTarget = false;
    private bool buttonJustClicked = false;

    public MeleeButtonScript(MeleeAttackData actionData)
    {
        this.actionData = actionData;
    }

    public override void Instruct(GenericButton button)
    {
        button.spriteRenderer.sprite = actionData.getSprite();
        player = GameObject.FindFirstObjectByType<PlayerScript>();
        slashIndicator = player.GetComponent<SlashDirectionIndicator>();
        actionUIManager = GameObject.FindFirstObjectByType<ActionUIManager>();
    }

    public override void Execute()
    {
        waitingForTarget = true;
        buttonJustClicked = true;
        slashIndicator.Show(actionUIManager.GetLastTargetPosition());
    }

    public override void Update()
    {
        if (buttonJustClicked) { buttonJustClicked = false; return; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            waitingForTarget = false;
            slashIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (waitingForTarget && Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            Vector2 direction = (targetPosition - actionUIManager.GetLastTargetPosition()).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 spawnPosition = actionUIManager.GetLastTargetPosition() + direction * spawnDistance;

            waitingForTarget = false;
            slashIndicator.Hide();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            // Fresh instance each time
            MeleeAttack action = (MeleeAttack)actionData.createAction();
            action.Initialize(spawnPosition, rotation);
            GameStateManager.Instance.newAction(action);
        }
    }
}