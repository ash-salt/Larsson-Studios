using Assets.Scripts.player_actions;
using UnityEngine;

namespace Assets.Scripts.player_actions {
public class FireballAction : AAction, Disposable
{
    private GameObject fireInstance;
    private Vector3 spawnPos;

    public FireballAction(ActionData actionData)
    {
        this.actionData = actionData;
    }

    public void Initialize(Vector3 spawnPos)
    {
        this.spawnPos = spawnPos;
    }

    public override void execute()
    {
        GameObject firePrefab = GameStateManager.Instance.GetSlashPrefab();
        fireInstance = GameObject.Instantiate(firePrefab, spawnPos, Quaternion.identity);
    }

    public void Dispose()
    {
        if (fireInstance != null) {
            Object.Destroy(fireInstance);
        }
    }
}
}