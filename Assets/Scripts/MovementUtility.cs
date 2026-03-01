using UnityEngine;

public static class MovementUtility
{
    private const float DEFAULT_COLLIDER_RADIUS = 0.3f;
    private const float SAFETY_MARGIN = 0.1f;
    
    public static Vector2 ValidateMovement(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        Vector2 toTarget = targetPosition - fromPosition;
        float distance = toTarget.magnitude;
        
        if (distance < 0.001f)
        {
            return fromPosition;
        }
        
        Vector2 direction = toTarget.normalized;
        float moveDistance = Mathf.Min(distance, maxDistance);
        
        Vector2 potentialTarget = fromPosition + direction * moveDistance;
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, moveDistance, LayerMask.GetMask("Obstacles"));
        
        float finalDistance = moveDistance;
        if (hit.collider != null)
        {
            finalDistance = Mathf.Max(hit.distance - (colliderRadius + SAFETY_MARGIN), 0f);
        }
        
        Vector2 finalPosition = fromPosition + direction * finalDistance;
        
        if (!IsPositionValid(finalPosition, colliderRadius))
        {
            return fromPosition;
        }
        
        return finalPosition;
    }
    
    public static bool IsPositionValid(Vector2 position, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        Collider2D overlap = Physics2D.OverlapCircle(position, colliderRadius, LayerMask.GetMask("Obstacles"));
        return overlap == null;
    }
    
    public static bool IsPositionReachable(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        float distance = Vector2.Distance(fromPosition, targetPosition);
        
        if (distance > maxDistance)
        {
            return false;
        }
        
        if (!IsPositionValid(targetPosition, colliderRadius))
        {
            return false;
        }
        
        Vector2 direction = (targetPosition - fromPosition).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, distance, LayerMask.GetMask("Obstacles"));
        
        return hit.collider == null;
    }
    
    public static Vector2 FindNearestValidPosition(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        if (IsPositionReachable(fromPosition, targetPosition, maxDistance, colliderRadius))
        {
            return targetPosition;
        }
        
        Vector2 toTarget = targetPosition - fromPosition;
        float distance = toTarget.magnitude;
        if (distance > maxDistance)
        {
            targetPosition = fromPosition + toTarget.normalized * maxDistance;
        }
        
        Vector2 direction = (targetPosition - fromPosition).normalized;
        float searchDistance = Vector2.Distance(fromPosition, targetPosition);
        
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, searchDistance, LayerMask.GetMask("Obstacles"));
        
        if (hit.collider != null)
        {
            float safeDistance = Mathf.Max(hit.distance - (colliderRadius + SAFETY_MARGIN), 0f);
            Vector2 safePosition = fromPosition + direction * safeDistance;
            
            if (IsPositionValid(safePosition, colliderRadius))
            {
                return safePosition;
            }
        }
        
        return fromPosition;
    }
}
