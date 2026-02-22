using UnityEngine;
using System.Collections.Generic;

public class ActionUIManager : MonoBehaviour
{
    public ActionSlot[] actionSlots;
    [SerializeField] private EntityScript playerScript;

    public GameObject moveIndicator;
    private List<GameObject> indicators = new List<GameObject>();
    private Vector2 playerPosition;

    public void Start()
    {
        updateMove();
    }

    public void UpdateActionUI(Sprite newSprite)
    {
        for (int i = 0; i < actionSlots.Length; i++)
        {
            if (i < playerScript.getActions().Length && actionSlots[i].GetActionSprite() == null)
            {   
                actionSlots[i].SetActionSprite(newSprite);
                return;
            }
        }
    }

    public void newMove(Vector2 targetPosition)
    {
        Vector2 originalPosition = playerPosition;
        playerPosition = targetPosition;
        GameObject indicator = Instantiate(moveIndicator);
        indicator.transform.position = targetPosition;
        indicators.Add(indicator);
    }
    
    public void updateMove()
    {
        playerPosition = playerScript.transform.position;
    }
    
    public void clearMove()
    {
        foreach (var indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();
    }

    public void clearActionUI()
    {
        foreach (var slot in actionSlots)
        {
            slot.ClearActionSprite();
        }
        clearMove();
    }

}
