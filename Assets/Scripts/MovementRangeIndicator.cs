using UnityEngine;


public class MovementRangeIndicator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private EntityScript entityScript;

    [SerializeField] private ActionUIManager actionUIManager;

    private bool isActive;



    public void Show()
    {
        isActive = true;
        lineRenderer.enabled = true;
    }

    public void Hide()
    {
        isActive = false;
        lineRenderer.enabled = false;
        
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        Vector2 currentPos = actionUIManager.GetLastTargetPosition();
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        Vector2 toMouse = mousePos2D - currentPos;
        float distance = toMouse.magnitude;

        if (distance < 0.001f)
        {
            Hide();
            return;
        }

        Vector2 direction = toMouse.normalized;
        float moveDistance = Mathf.Min(distance, entityScript.maxMoveDistance);

        
        Vector2 potentialTarget = currentPos + direction * moveDistance;

        
        RaycastHit2D hit = Physics2D.Linecast(currentPos, potentialTarget, LayerMask.GetMask("Obstacles"));

        float finalDistance = moveDistance;
        if (hit.collider != null)
        {
            
            float hitDistance = Vector2.Distance(currentPos, hit.point);
            finalDistance = Mathf.Max(hitDistance - 0.05f, 0f);
        }


        Vector2 finalTarget = currentPos + direction * finalDistance;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(currentPos.x, currentPos.y, -0.5f));
        lineRenderer.SetPosition(1, new Vector3(finalTarget.x, finalTarget.y, -0.5f));

    }
}