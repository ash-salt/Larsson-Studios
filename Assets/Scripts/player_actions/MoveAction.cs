using Assets.Scripts.player_actions;
using UnityEngine;


namespace Assets.Scripts.player_actions {
public class MoveAction : AAction
{
    private Vector2 targetPosition;
    private Vector2 startPosition;
    private float maxDistance;
    private GameObject moveAnimationInstance;
    public MoveAction(ActionData data)
    {
        actionData = data;
    }

    public void Initialize(Vector2 targetPosition, float maxDistance, Vector2 startPosition)
    {
        this.targetPosition = targetPosition;
        this.maxDistance = maxDistance;
        this.startPosition = startPosition;
    }

    public Vector2 getTargetPosition()
    {
        return targetPosition;
    }  

    public Vector2 getStartPosition()
    {
        return startPosition;
    }

    public override void execute()
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        Vector2 currentPos = rb.position;

        float colliderRadius = 0.3f;
        CircleCollider2D circleCollider = target.GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            colliderRadius = circleCollider.radius;
        }
        else
        {
            BoxCollider2D boxCollider = target.GetComponent<BoxCollider2D>();
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
            target.doneWithAction();
            return;
        }

        GameObject moveAnimPrefab = GameStateManager.Instance.GetMoveAnimationPrefab();

        if (moveAnimPrefab != null)
        {
            moveAnimationInstance = GameObject.Instantiate(moveAnimPrefab, target.transform.position, Quaternion.identity);
            MoveAnimationScript moveAnim = moveAnimationInstance.GetComponent<MoveAnimationScript>();

            if (moveAnim != null)
            {
                moveAnim.StartMove(target, finalPos, () => target.doneWithAction());
            }
            else
            {
                rb.MovePosition(finalPos);
                target.doneWithAction();
            }
        }
        else
        {
            rb.MovePosition(finalPos);
            target.doneWithAction();
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
}