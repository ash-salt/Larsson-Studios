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
        // Extract current 2D position (XY plane)
        Vector2 currentPos = new Vector2(entity.transform.position.x, entity.transform.position.y);

        // Calculate distance to target
        float distance = Vector2.Distance(currentPos, targetPosition);

        // Clamp to max distance if target is too far
        Vector2 validatedTarget = targetPosition;
        if (distance > maxDistance)
        {
            Vector2 direction = (targetPosition - currentPos).normalized;
            validatedTarget = currentPos + direction * maxDistance;
        }

        // Apply movement (preserve Z coordinate)
        entity.transform.position = new Vector3(validatedTarget.x, validatedTarget.y, entity.transform.position.z);
        
        entity.doneWithAction();
        MonoBehaviour.print("Moved to: " + validatedTarget);
    }
}
