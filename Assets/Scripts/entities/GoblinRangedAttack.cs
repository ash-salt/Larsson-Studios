using Assets.Scripts.player_actions;
using UnityEngine;
public class GoblinRangedAttack : AAction
{
    private bool prepared;
    private Vector2 playerPos;
    private LayerMask obstacleLayer;
    private LayerMask playerLayer;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private float objectDistance;
    private float playerDistance;

    public GoblinRangedAttack(ActionData actionData)
    {
        this.actionData = actionData;
        obstacleLayer = LayerMask.GetMask("Obstacles");
        playerLayer = LayerMask.GetMask("Player");
    }

    public void Initialize(bool isPrepared, Vector2 playerPos)
    {
        this.prepared = isPrepared;
        this.playerPos = playerPos;
    }

    public override void execute()
{
    spriteRenderer = target.GetComponent<SpriteRenderer>();
    GoblinRangedData data = (GoblinRangedData)actionData;

    if (!prepared)
    {
        spriteRenderer.sprite = data.attackSprite;
        target.doneWithAction();
        return;
    }

    lineRenderer = target.GetComponent<LineRenderer>();
    lineRenderer.useWorldSpace = true;
    lineRenderer.enabled = true;

    Vector2 origin = (Vector2)target.transform.position;
    Vector2 direction = (playerPos - origin).normalized;
    origin += direction * 0.5f;
    float distance = Vector2.Distance(origin, playerPos) * 1.5f;

    RaycastHit2D wallHit = Physics2D.Raycast(origin, direction, distance, obstacleLayer);
    RaycastHit2D playerHit = Physics2D.Raycast(origin, direction, distance, playerLayer);

    Vector3 startPos = new Vector3(target.transform.position.x, target.transform.position.y, -0.5f);
    lineRenderer.positionCount = 2;
    lineRenderer.SetPosition(0, startPos);

    bool wallBlocking = wallHit.collider != null &&
                        (playerHit.collider == null || wallHit.distance < playerHit.distance);

    if (wallBlocking)
    {
        Debug.Log("Shot hit a wall");
        lineRenderer.SetPosition(1, new Vector3(wallHit.point.x, wallHit.point.y, -0.5f));
    }
    else if (playerHit.collider != null)
    {
        lineRenderer.SetPosition(1, new Vector3(playerHit.point.x, playerHit.point.y, -0.5f));
        EntityScript hitEntity = playerHit.collider.GetComponent<EntityScript>();
        if (!hitEntity.isBlocking)
        {
            hitEntity.damage(25);
        }
        else
        {
            Debug.Log("Shot was blocked!");
        }
    }
    else
    {
        Vector2 endpoint = origin + direction * distance;
        lineRenderer.SetPosition(1, new Vector3(endpoint.x, endpoint.y, -0.5f));
    }

    spriteRenderer.sprite = data.defaultSprite;
    target.doneWithAction();
}
    public void Dispose()
    {
        if (lineRenderer != null) {
            lineRenderer.enabled = false;
        }
    }
}