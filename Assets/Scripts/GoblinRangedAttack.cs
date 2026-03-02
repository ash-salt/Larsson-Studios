using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinRangedAttack : IAction 
{
    private bool prepared;
    public PlayerScript player;
    private LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
    private LayerMask playerLayer = LayerMask.GetMask("Ignore Raycast");

    public LineRenderer lineRenderer;
    private Vector2 playerPos;

    private SpriteRenderer spriteRenderer;
    public Sprite attackSprite;
    public Sprite defaultSprite;

    public GoblinRangedAttack(bool isPrepared, Vector3 pPos)
    {
        prepared = isPrepared;
        playerPos = pPos;

        attackSprite = GameStateManager.Instance.archerAttackSprite;
        defaultSprite = GameStateManager.Instance.archerDefaultSprite;
        
    }
    public int getCost()
    {
        return 1;
    }

    public int getCooldown()
    {
        return 0;
    }

    public void execute(EntityScript entity)
    {
        spriteRenderer = entity.GetComponent<SpriteRenderer>();
        Debug.Log("prepared is " + prepared);
        if (prepared == false)
        {
            spriteRenderer.sprite = attackSprite;
        }
        else
        {
            lineRenderer = entity.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = true;
            MonoBehaviour.print("firing!");
            Vector2 origin = entity.transform.position;
            Vector2 direction = (playerPos - origin).normalized;
            origin = origin + direction*0.5f;
            float distance = Vector2.Distance(origin, playerPos) * 1.5f;

            Debug.Log("Origin: " + origin);
            Debug.Log("PlayerPos: " + playerPos);
            Debug.Log("Direction: " + direction);
            Debug.Log("Distance: " + distance);

            RaycastHit2D hit = Physics2D.Raycast(
                origin,
                direction,
                distance,
                obstacleLayer
            );

            if (hit.collider != null)
            {
                // Hit wall/obstacle
                Debug.Log("hit object");
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, new Vector3(entity.transform.position.x, entity.transform.position.y, -0.5f));
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -0.5f));
            }
            else
            {
                Debug.DrawRay(origin, direction * distance, Color.red, 1f);
                // No obstacle → check player
                RaycastHit2D entityHit = Physics2D.Raycast(
                    origin,
                    direction,
                    distance,
                    playerLayer
                );

                if (entityHit.collider != null)
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, new Vector3(entity.transform.position.x, entity.transform.position.y, -0.5f));
                    lineRenderer.SetPosition(1, new Vector3(entityHit.point.x, entityHit.point.y, -0.5f));
                    if (!entityHit.collider.GetComponent<EntityScript>().isBlocking)
                    {
                        Debug.Log("hit something named " + entityHit.collider.name);
                        entityHit.collider.GetComponent<EntityScript>().damage(25);
                    }
                    else
                    {
                        Debug.Log("Shot was blocked!!!");
                    }
                }
            }
            spriteRenderer.sprite = defaultSprite;
        }
    }
    
    public void Dispose()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;  
        }
        
    }
}
