using UnityEngine;


public class MovementRangeIndicator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private EntityScript entityScript;

    private bool isActive;
    private Vector2 startPosition;



    public void Show(Vector2 fromPosition)
    {
        isActive = true;
        startPosition = fromPosition;
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
        Vector2 mousePos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // Use MovementUtility to validate the movement
        Vector2 finalTarget = MovementUtility.ValidateMovement(
            startPosition, 
            mousePos2D, 
            entityScript.maxMoveDistance
        );

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(startPosition.x, startPosition.y, -0.5f));
        lineRenderer.SetPosition(1, new Vector3(finalTarget.x, finalTarget.y, -0.5f));

    }
}