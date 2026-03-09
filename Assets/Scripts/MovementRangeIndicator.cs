using UnityEngine;

public class MovementRangeIndicator : MonoBehaviour
{
    [SerializeField] private EntityScript entityScript;
    [SerializeField] private int rayCount = 72;
    [SerializeField] private LayerMask obstacleLayer;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private bool isActive;
    private Vector2 originPosition;
    private float maxRange;
    private float colliderRadius;
    [SerializeField] private SpriteRenderer cursorSpriteRenderer;
    private Vector2[] meshWorldPolygon;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        Material mat = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default"))
        {
            color = new Color(0.5f, 0.5f, 0.5f, 0.1f) 
        };

        meshRenderer.material = mat;
        //meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Unlit-Default"));
        meshRenderer.sortingLayerName = "Ignore Raycast";
        meshRenderer.sortingOrder = 1;
        meshRenderer.enabled = false;
    }

    public void Show(Vector2 from)
    {
        originPosition = from;
        isActive = true;
        meshRenderer.enabled = true;
        transform.position = originPosition;
        maxRange = entityScript.maxMoveDistance;
        CircleCollider2D circleCollider = entityScript.GetComponent<CircleCollider2D>();
        colliderRadius = circleCollider.radius;
        BuildMesh();
    }

    public void Show(Vector2 from, float maxRange, float collider)
    {
        originPosition = from;
        isActive = true;
        meshRenderer.enabled = true;
        transform.position = originPosition;
        this.maxRange = maxRange;
        this.colliderRadius = collider;
        BuildMesh();
    }

    public void Hide()
    {
        isActive = false;
        meshRenderer.enabled = false;
        cursorSpriteRenderer.enabled = false; 
    }

    void Update()
    {
        if (isActive)
        {
            BuildMesh();
            UpdateCursorIndicator();
        }
    }

    private void ChangeCursor(Sprite newCursor)
    {
        cursorSpriteRenderer.sprite = newCursor;
    }

    private void UpdateCursorIndicator()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (meshWorldPolygon != null && IsPointInPolygon(mouseWorld, meshWorldPolygon))
        {
            cursorSpriteRenderer.enabled = (true);
            cursorSpriteRenderer.transform.position = mouseWorld;
        }
        else
        {
            cursorSpriteRenderer.enabled = (false);
        }
    }

    private bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
    {
        int count = polygon.Length;
        bool inside = false;

        for (int i = 0, j = count - 1; i < count; j = i++)
        {
            float xi = polygon[i].x, yi = polygon[i].y;
            float xj = polygon[j].x, yj = polygon[j].y;

            bool intersects = ((yi > point.y) != (yj > point.y))
                && (point.x < (xj - xi) * (point.y - yi) / (yj - yi) + xi);

            if (intersects) inside = !inside;
        }

        return inside;
    }

    private void BuildMesh()
    {
        int vertCount = rayCount + 2;
        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[rayCount * 3];
        meshWorldPolygon = new Vector2[rayCount + 1];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = (float)i / rayCount * Mathf.PI * 2f;
            Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.CircleCast(originPosition, colliderRadius, dir, maxRange, obstacleLayer);
    
            Vector2 worldPoint;
            if (hit.collider != null)
            {
                float adjustedDistance = Mathf.Max(hit.distance - 0.1f, 0f);
                worldPoint = originPosition + dir * adjustedDistance;
            }
            else
            {
                worldPoint = originPosition + dir * maxRange;
            }

            Vector2 localPoint = worldPoint - originPosition;
            vertices[i + 1] = localPoint;
            if (i < rayCount)
                meshWorldPolygon[i] = worldPoint;
        }

        for (int i = 0; i < rayCount; i++)
        {
            triangles[i * 3 + 0] = 0;
            triangles[i * 3 + 1] = i + 2;
            triangles[i * 3 + 2] = i + 1;
        }

        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
        
    }
}