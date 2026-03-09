using Assets.Scripts.player_actions;
using UnityEngine;

public class MagicMissile : AAction, Indicatable
{
    private Vector2 targetPos;
    private LayerMask obstacleLayer;
    private LayerMask enemyLayer;
    private float objectDistance;
    private float enemyDistance;
    private GameObject spawnedMissile;
    private GameObject indicator;
    private Quaternion rotation;

    public MagicMissile(ActionData actionData)
    {
        this.actionData = actionData;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        enemyLayer = LayerMask.GetMask("Enemy");
        this.indicator = ((MagicMissileData) actionData).indicator;
    }

    public void Initialize(Vector2 targetPos)
    {
        this.targetPos = targetPos;
    }

    public GameObject getIndicator()
    {
        return indicator;
    }

    public override void execute()
{
    MagicMissileData data = (MagicMissileData)actionData;

    Vector2 origin = (Vector2)target.transform.position;
    Vector2 direction = (targetPos - origin).normalized;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    Vector3 spawnPos = new Vector3(origin.x, origin.y, -0.5f);

    spawnedMissile = Object.Instantiate(data.MagicPrefab, spawnPos, rotation);

    Collider2D[] playerColliders = target.GetComponents<Collider2D>();
    Collider2D missileCollider = spawnedMissile.GetComponent<Collider2D>();
    foreach (Collider2D col in playerColliders)
    {
        Physics2D.IgnoreCollision(missileCollider, col);
    }

    MissilePrefab projectile = spawnedMissile.GetComponent<MissilePrefab>();
    if (projectile != null)
    {
        projectile.Launch(direction, obstacleLayer, enemyLayer, () => target.doneWithAction());
    }
    else
    {
        Debug.LogWarning("MagicPrefab is missing a MagicMissileProjectile component!");
        target.doneWithAction();
    }
}

}