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
        BuildMesh();
    }

    public void Show(Vector2 from, float maxRange)
    {
        originPosition = from;
        isActive = true;
        meshRenderer.enabled = true;
        transform.position = originPosition;
        this.maxRange = maxRange;
        BuildMesh();
    }

    public void Hide()
    {
        isActive = false;
        meshRenderer.enabled = false;
    }

    void Update()
    {
        if (isActive)
            BuildMesh();

    }

    private void BuildMesh()
    {
        int vertCount = rayCount + 2;
        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[rayCount * 3];

        CircleCollider2D circleCollider = entityScript.GetComponent<CircleCollider2D>();
        float colliderRadius = circleCollider.radius;
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