using Assets.Scripts.player_actions;
using UnityEngine;


namespace Assets.Scripts.player_actions {
public class MeleeAttack : AAction, Disposable
{
    private GameObject slashInstance;
    private Vector3 spawnPos;
    private Quaternion rotation;

    public MeleeAttack(ActionData actionData)
    {
        this.actionData = actionData;
    }

    public void Initialize(Vector3 spawnPos, Quaternion rotation)
    {
        this.spawnPos = spawnPos;
        this.rotation = rotation;
    }

    public override void execute()
    {
        GameObject slashPrefab = GameStateManager.Instance.GetSlashPrefab();
        slashInstance = GameObject.Instantiate(slashPrefab, spawnPos, rotation);
    }

    public void Dispose()
    {
        if (slashInstance != null) {
            Object.Destroy(slashInstance);
        }
    }
}
}