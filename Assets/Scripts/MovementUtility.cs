using UnityEngine;

public static class MovementUtility
{
    public static Vector2 ValidateMovement(Vector2 fromPosition, Vector2 targetPosition, float maxDistance)
    {
        Vector2 toTarget = targetPosition - fromPosition;
        float distance = toTarget.magnitude;
        
        if (distance < 0.001f)
        {
            return fromPosition; // No movement
        }
        
        Vector2 direction = toTarget.normalized;
        float moveDistance = Mathf.Min(distance, maxDistance);
        
        // Check for obstacles
        Vector2 potentialTarget = fromPosition + direction * moveDistance;
        RaycastHit2D hit = Physics2D.Linecast(fromPosition, potentialTarget, LayerMask.GetMask("Obstacles"));
        
        float finalDistance = moveDistance;
        if (hit.collider != null)
        {
            float hitDistance = Vector2.Distance(fromPosition, hit.point);
            finalDistance = Mathf.Max(hitDistance - 0.05f, 0f);
        }
        
        return fromPosition + direction * finalDistance;
    }
}
