using Assets.Scripts.player_actions;
using UnityEngine;

public class MoveAction : IAction
{
    private Vector2 targetPosition;
    
    private Vector2 startPosition;
    private float maxDistance;
    GameObject moveAnimationInstance;

    public MoveAction(Vector2 targetPosition, float maxDistance, Vector2 startPosition)
    {
        this.targetPosition = targetPosition;
        this.maxDistance = maxDistance;
        this.startPosition = startPosition;
    }

    public Vector2 getTargetPosition()
    {
        return targetPosition;
    }  

    public int getCooldown()
    {
        return 0;
    }

    public int getCost()
    {
        return 1;
    }

    public Vector2 getStartPosition()
    {
        return startPosition;
    }

    public void execute(EntityScript entity)
    {
        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();

        Vector2 currentPos = rb.position;

        Vector2 toTarget = targetPosition - currentPos;
        float distance = toTarget.magnitude;

        if (distance <= 0.001f)
        {
            entity.doneWithAction();
            return;
        }

        Vector2 direction = toTarget.normalized;

        float moveDistance = Mathf.Min(distance, maxDistance);

        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.SetLayerMask(LayerMask.GetMask("Obstacles"));
        filter.useTriggers = false;

        RaycastHit2D[] hits = new RaycastHit2D[8];

        int hitCount = rb.Cast(direction, filter, hits, moveDistance);

        float finalDistance = moveDistance;

        if (hitCount > 0)
        {
            finalDistance = Mathf.Max(hits[0].distance - 0.05f, 0f);
        }

        Vector2 finalPos = currentPos + direction * finalDistance;

        GameObject moveAnimPrefab = GameStateManager.Instance.GetMoveAnimationPrefab();

        if (moveAnimPrefab != null)
        {
            moveAnimationInstance = GameObject.Instantiate(moveAnimPrefab, entity.transform.position, Quaternion.identity);
            MoveAnimationScript moveAnim = moveAnimationInstance.GetComponent<MoveAnimationScript>();

            if (moveAnim != null)
            {
                moveAnim.StartMove(entity, finalPos, () => entity.doneWithAction());
            }
            else
            {
                rb.MovePosition(finalPos);
                entity.doneWithAction();
            }
        }
        else
        {
            rb.MovePosition(finalPos);
            entity.doneWithAction();
        }
    }

    public void Dispose()
    {
        if (moveAnimationInstance != null)
        {
            Object.Destroy(moveAnimationInstance);
        }
    }

}
