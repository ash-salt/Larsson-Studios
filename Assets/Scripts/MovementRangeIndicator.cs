using UnityEngine;
using System.Collections.Generic;

public class MovementRangeIndicator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EntityScript entityScript;
    [SerializeField] private int circleSegments = 64; // Number of points to sample around circle
    [SerializeField] private float colliderRadius = 0.3f; // Player collider size
    
    private bool isActive;
    private Vector2 startPosition;
    
    void Start()
    {
        // Try to get collider radius from entity if available
        if (entityScript != null)
        {
            CircleCollider2D circleCollider = entityScript.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                colliderRadius = circleCollider.radius;
            }
            else
            {
                BoxCollider2D boxCollider = entityScript.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    // Use average of width and height for radius approximation
                    colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
                }
            }
        }
        
        // Set LineRenderer to loop mode for circle
        if (lineRenderer != null)
        {
            lineRenderer.loop = false; // We'll manually handle gaps for blocked areas
        }
    }

    public void Show(Vector2 fromPosition)
    {
        isActive = true;
        startPosition = fromPosition;
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    public void Hide()
    {
        isActive = false;
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (!isActive || entityScript == null)
        {
            return;
        }

        DrawCircularRange();
    }
    
    private void DrawCircularRange()
    {
        List<Vector3> validPoints = new List<Vector3>();
        float maxDistance = entityScript.maxMoveDistance;
        bool wasLastPointValid = false;
        
        // Sample points around the circle
        for (int i = 0; i <= circleSegments; i++) // Include endpoint to close circle
        {
            float angle = (float)i / circleSegments * 360f * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            // Check if we can reach this direction at max distance
            // Use simpler check: just see if path is clear, don't check final position overlap
            RaycastHit2D hit = Physics2D.CircleCast(
                startPosition, 
                colliderRadius, 
                direction, 
                maxDistance, 
                LayerMask.GetMask("Obstacles")
            );
            
            bool isReachable = (hit.collider == null);
            Vector2 targetPoint;
            
            if (isReachable)
            {
                // Full distance is reachable
                targetPoint = startPosition + direction * maxDistance;
            }
            else
            {
                // Partial distance is reachable - stop before obstacle
                float reachableDistance = Mathf.Max(hit.distance - (colliderRadius * 0.5f), 0f);
                targetPoint = startPosition + direction * reachableDistance;
                isReachable = (reachableDistance > 0.1f); // Only count if we can move at least a bit
            }
            
            if (isReachable)
            {
                validPoints.Add(new Vector3(targetPoint.x, targetPoint.y, -0.5f));
                wasLastPointValid = true;
            }
            else
            {
                // Create gap in the line
                if (wasLastPointValid && validPoints.Count > 0)
                {
                    // Add NaN to mark gap (LineRenderer will handle this)
                    validPoints.Add(new Vector3(float.NaN, float.NaN, float.NaN));
                }
                wasLastPointValid = false;
            }
        }
        
        // Update LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = validPoints.Count;
            if (validPoints.Count > 0)
            {
                lineRenderer.SetPositions(validPoints.ToArray());
            }
        }
    }
}