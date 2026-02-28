using UnityEngine;

public class PositionIndicatorSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private EntityScript entityScript;
    [SerializeField] private float colliderRadius = 0.3f;
    
    private bool isActive;
    private Vector2 startPosition;
    
    void Awake()
    {
        // Find components if not set
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        if (entityScript == null)
        {
            // Look for entity script in parent
            entityScript = GetComponentInParent<EntityScript>();
        }
        
        // Start hidden
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    
    void Start()
    {
        // Try to get collider radius from entity if available
        if (entityScript != null)
        {
            CircleCollider2D circleCollider = entityScript.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                colliderRadius = circleCollider.radius;
                Debug.Log($"PositionIndicator: Using CircleCollider radius {colliderRadius}");
            }
            else
            {
                BoxCollider2D boxCollider = entityScript.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    // Use average of width and height for radius approximation
                    colliderRadius = (boxCollider.size.x + boxCollider.size.y) / 4f;
                    Debug.Log($"PositionIndicator: Using BoxCollider radius approximation {colliderRadius}");
                }
            }
        }
        else
        {
            Debug.LogWarning("PositionIndicatorSprite: EntityScript not found!");
        }
    }
    
    public void Show(Vector2 fromPosition)
    {
        isActive = true;
        startPosition = fromPosition;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            Debug.Log($"PositionIndicator shown at {fromPosition}");
        }
        else
        {
            Debug.LogError("PositionIndicatorSprite: SpriteRenderer is null!");
        }
    }
    
    public void Hide()
    {
        isActive = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        if (entityScript == null)
        {
            Debug.LogWarning("PositionIndicatorSprite: EntityScript is null in Update!");
            return;
        }
        
        if (Camera.main == null)
        {
            Debug.LogWarning("PositionIndicatorSprite: Main camera not found!");
            return;
        }
        
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        
        // Find nearest valid position to mouse cursor
        Vector2 validPosition = MovementUtility.FindNearestValidPosition(
            startPosition,
            mousePos2D,
            entityScript.maxMoveDistance,
            colliderRadius
        );
        
        // Update sprite position (keep Z at -0.5 for visibility)
        transform.position = new Vector3(validPosition.x, validPosition.y, -0.5f);
    }
}
