using UnityEngine;

public class MoveAction : IAction
{
    private Vector2 targetPosition;
    private float maxDistance;

    public MoveAction(Vector2 targetPosition, float maxDistance)
    {
        this.targetPosition = targetPosition;
        this.maxDistance = maxDistance;
    }

    public int getCost()
    {
        return 1;
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
        filter.useLayerMask = true; // REQUIRED
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

        rb.MovePosition(finalPos);

        entity.doneWithAction();
    }

}
