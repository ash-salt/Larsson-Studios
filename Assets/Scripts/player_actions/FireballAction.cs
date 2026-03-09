using Assets.Scripts.player_actions;
using UnityEngine;

namespace Assets.Scripts.player_actions {
public class FireballAction : AAction, Disposable, Indicatable
{
    private GameObject fireInstance;
    private Vector3 spawnPos;
    private GameObject indicator;

    public FireballAction(ActionData actionData)
    {
        this.actionData = actionData;
        this.indicator = ((FireballActionData) actionData).indicator;
    }

    public void Initialize(Vector3 spawnPos)
    {
        this.spawnPos = spawnPos;
    }

    public GameObject getIndicator()
    {
        return indicator;
    }

    public override void execute()
    {
        GameObject firePrefab = GameStateManager.Instance.GetExplosionPrefab();
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