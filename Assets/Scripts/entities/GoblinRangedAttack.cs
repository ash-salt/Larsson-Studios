using Assets.Scripts.player_actions;
using UnityEngine;
public class GoblinRangedAttack : AAction
{
    private bool prepared;
    private Vector2 playerPos;
    private LayerMask obstacleLayer;
    private LayerMask playerLayer;
    private SpriteRenderer spriteRenderer;
    private float objectDistance;
    private float playerDistance;
    private GameObject arrowPrefab;

    public GoblinRangedAttack(ActionData actionData)
    {
        this.actionData = (GoblinRangedData) actionData;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        playerLayer = LayerMask.GetMask("Player");  
        arrowPrefab = ((GoblinRangedData) actionData).ArrowPrefab;
    }

    public void Initialize(bool isPrepared, Vector2 playerPos)
    {
        this.prepared = isPrepared;
        this.playerPos = playerPos;
    }

    public override void execute()
    {
     
    GoblinRangedData data = ((GoblinRangedData) actionData);
    spriteRenderer = target.GetComponent<SpriteRenderer>();
    if (!prepared)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = data.attackSprite;
        }
        target.doneWithAction();
        return;
    }

    Vector2 origin = (Vector2)target.transform.position;
    Vector2 direction = (playerPos - origin).normalized;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    Vector3 spawnPos = new Vector3(origin.x, origin.y, -0.5f);

    arrowPrefab = Object.Instantiate(data.ArrowPrefab, spawnPos, rotation);

    Collider2D[] goblinColliders = target.GetComponents<Collider2D>();
    Collider2D arrowCollider = arrowPrefab.GetComponent<Collider2D>();
    foreach (Collider2D col in goblinColliders)
    {
        Physics2D.IgnoreCollision(arrowCollider, col);
    }

    ArcherArrow projectile = arrowPrefab.GetComponent<ArcherArrow>();
    if (projectile != null)
    {
        projectile.Launch(direction, obstacleLayer, playerLayer, () => target.doneWithAction());
    }
    else
    {
        target.doneWithAction();
    }
    if (spriteRenderer != null)
    {
        spriteRenderer.sprite = data.defaultSprite;
    }
}
}