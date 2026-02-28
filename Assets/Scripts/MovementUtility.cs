using UnityEngine;

public static class MovementUtility
{
    private const float DEFAULT_COLLIDER_RADIUS = 0.3f; // Default player collider size
    private const float SAFETY_MARGIN = 0.1f; // Increased from 0.05f
    
    public static Vector2 ValidateMovement(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        Vector2 toTarget = targetPosition - fromPosition;
        float distance = toTarget.magnitude;
        
        if (distance < 0.001f)
        {
            return fromPosition; // No movement
        }
        
        Vector2 direction = toTarget.normalized;
        float moveDistance = Mathf.Min(distance, maxDistance);
        
        // Use CircleCast to account for player collider size
        Vector2 potentialTarget = fromPosition + direction * moveDistance;
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, moveDistance, LayerMask.GetMask("Obstacles"));
        
        float finalDistance = moveDistance;
        if (hit.collider != null)
        {
            // Use larger safety margin accounting for collider radius
            finalDistance = Mathf.Max(hit.distance - (colliderRadius + SAFETY_MARGIN), 0f);
        }
        
        Vector2 finalPosition = fromPosition + direction * finalDistance;
        
        // Final check: ensure destination has clearance
        if (!IsPositionValid(finalPosition, colliderRadius))
        {
            return fromPosition; // Can't move if destination is blocked
        }
        
        return finalPosition;
    }
    
    /// <summary>
    /// Check if a position is valid (not overlapping with obstacles)
    /// </summary>
    public static bool IsPositionValid(Vector2 position, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        Collider2D overlap = Physics2D.OverlapCircle(position, colliderRadius, LayerMask.GetMask("Obstacles"));
        return overlap == null;
    }
    
    /// <summary>
    /// Check if a position is reachable from start position
    /// </summary>
    public static bool IsPositionReachable(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        float distance = Vector2.Distance(fromPosition, targetPosition);
        
        // Check distance
        if (distance > maxDistance)
        {
            return false;
        }
        
        // Check if destination is valid
        if (!IsPositionValid(targetPosition, colliderRadius))
        {
            return false;
        }
        
        // Check if path is clear
        Vector2 direction = (targetPosition - fromPosition).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, distance, LayerMask.GetMask("Obstacles"));
        
        return hit.collider == null;
    }
    
    /// <summary>
    /// Find the nearest valid position to targetPosition that is reachable
    /// </summary>
    public static Vector2 FindNearestValidPosition(Vector2 fromPosition, Vector2 targetPosition, float maxDistance, float colliderRadius = DEFAULT_COLLIDER_RADIUS)
    {
        // If target is already valid and reachable, return it
        if (IsPositionReachable(fromPosition, targetPosition, maxDistance, colliderRadius))
        {
            return targetPosition;
        }
        
        // Clamp to max distance first
        Vector2 toTarget = targetPosition - fromPosition;
        float distance = toTarget.magnitude;
        if (distance > maxDistance)
        {
            targetPosition = fromPosition + toTarget.normalized * maxDistance;
        }
        
        // Try to find a valid position by backing off from obstacles
        Vector2 direction = (targetPosition - fromPosition).normalized;
        float searchDistance = Vector2.Distance(fromPosition, targetPosition);
        
        RaycastHit2D hit = Physics2D.CircleCast(fromPosition, colliderRadius, direction, searchDistance, LayerMask.GetMask("Obstacles"));
        
        if (hit.collider != null)
        {
            // Back off from the obstacle
            float safeDistance = Mathf.Max(hit.distance - (colliderRadius + SAFETY_MARGIN), 0f);
            Vector2 safePosition = fromPosition + direction * safeDistance;
            
            if (IsPositionValid(safePosition, colliderRadius))
            {
                return safePosition;
            }
        }
        
        // If all else fails, return start position
        return fromPosition;
    }
}
