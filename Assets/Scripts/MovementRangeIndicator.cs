using UnityEngine;
using System.Collections.Generic;

public class MovementRangeIndicator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EntityScript entityScript;
    [SerializeField] private int circleSegments = 64;
    [SerializeField] private float colliderRadius = 0.3f;
    
    private bool isActive;
    private Vector2 startPosition;
    
    void Start()
    {
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
                    colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
                }
            }
        }
        
        if (lineRenderer != null)
        {
            lineRenderer.loop = false;
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
        
        for (int i = 0; i <= circleSegments; i++)
        {
            float angle = (float)i / circleSegments * 360f * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
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
                targetPoint = startPosition + direction * maxDistance;
            }
            else
            {
                float reachableDistance = Mathf.Max(hit.distance - (colliderRadius * 0.5f), 0f);
                targetPoint = startPosition + direction * reachableDistance;
                isReachable = (reachableDistance > 0.1f);
            }
            
            if (isReachable)
            {
                validPoints.Add(new Vector3(targetPoint.x, targetPoint.y, -0.5f));
                wasLastPointValid = true;
            }
            else
            {
                if (wasLastPointValid && validPoints.Count > 0)
                {
                    validPoints.Add(new Vector3(float.NaN, float.NaN, float.NaN));
                }
                wasLastPointValid = false;
            }
        }
        
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