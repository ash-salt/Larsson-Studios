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

        float colliderRadius = 0.3f;
        CircleCollider2D circleCollider = entity.GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            colliderRadius = circleCollider.radius;
        }
        else
        {
            BoxCollider2D boxCollider = entity.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
            }
        }

        Vector2 finalPos = MovementUtility.ValidateMovement(
            currentPos, 
            targetPosition, 
            maxDistance, 
            colliderRadius
        );

        float moveDistance = Vector2.Distance(currentPos, finalPos);
        if (moveDistance <= 0.001f)
        {
            entity.doneWithAction();
            return;
        }

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
