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
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        if (entityScript == null)
        {
            entityScript = GetComponentInParent<EntityScript>();
        }
        
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
    
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
    }
    
    public void Show(Vector2 fromPosition)
    {
        isActive = true;
        startPosition = fromPosition;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
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
            return;
        }
        
        if (Camera.main == null)
        {
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
