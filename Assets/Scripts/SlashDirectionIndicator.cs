using UnityEngine;

public class SlashDirectionIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private EntityScript entityScript;

    private bool isActive;
    private Vector2 startPosition;

    float spawnDistance = 0.75f;

    GameObject ghostSlashInstance;

    GameObject slashPrefab;



    void Start()
    {
        slashPrefab = GameStateManager.Instance.ghostSlashPrefab;
    }

    public void Show(Vector2 fromPosition)
    {
        isActive = true;
        startPosition = fromPosition;
    }

    public void Hide()
    {
        isActive = false;
        
    }

    void Update()
    {
        GameObject.Destroy(ghostSlashInstance);
        if (!isActive)
        {
            return;
        }
        

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // Use MovementUtility to validate the movement
        Vector2 direction = (mousePos2D - startPosition).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector3 spawnPosition = startPosition + direction * spawnDistance;
        ghostSlashInstance = GameObject.Instantiate(slashPrefab, spawnPosition, rotation);

    }
}
