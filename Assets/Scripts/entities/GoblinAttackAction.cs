using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinAttackAction : AAction
{
    private float spawnDistance = 0.5f;
    private GameObject slashInstance;

    public GoblinAttackAction(ActionData actionData)
    {
        this.actionData = actionData;
    }

    public override void execute()
    {
        EntityScript player = null;
        foreach (EntityScript entity in GameStateManager.Instance.GetEntityList())
        {
            if (entity is PlayerScript)
            {
                player = entity;
                break;
            }
        }

        if (player == null) return;

        GameObject slashPrefab = GameStateManager.Instance.GetGoblinSlashPrefab();
        Vector2 direction = (player.transform.position - target.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 spawnPosition = target.transform.position + (Vector3)direction * spawnDistance;

        slashInstance = GameObject.Instantiate(slashPrefab, spawnPosition, rotation);
        target.doneWithAction();
    }

    public void Dispose()
    {
        if (slashInstance != null)
            Object.Destroy(slashInstance);
    }
}


