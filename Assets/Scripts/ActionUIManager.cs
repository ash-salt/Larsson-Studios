using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.player_actions;

public class ActionUIManager : MonoBehaviour
{
    public ActionSlot[] actionSlots;

    [SerializeField] public PlayerScript playerScript;

    public GameObject moveIndicator;
    private List<GameObject> indicators = new List<GameObject>();
    private Vector2 playerPosition;

    public void Start()
    {
        updateMove();
    }

    public void UpdateActionUI(Sprite newSprite)
    {
    ActionSlot emptySlot = firstSlot();
        if (emptySlot != null)
        {
            emptySlot.SetActionSprite(newSprite);
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

    public void undoMove()
    {
    IAction action = playerScript.DequeueAction();
    if (action == null) return;

    if (indicators.Count > 0 && action is MoveAction)
    {
        playerPosition = (action as MoveAction).startPosition;
        GameObject lastIndicator = indicators[indicators.Count - 1];
        Destroy(lastIndicator);
        indicators.RemoveAt(indicators.Count - 1);    
    }

    for (int i = actionSlots.Length - 1; i >= 0; i--)
        {
            if (actionSlots[i].GetActionSprite() != null)
                {
                    actionSlots[i].ClearActionSprite();
                    break;
                }
        }
    }

    public Vector2 GetLastTargetPosition()
    {
        return playerPosition;
    }
    
    public void clearMove()
    {
        foreach (var indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();
    }

    private ActionSlot firstSlot()
{
    for (int i = 0; i < actionSlots.Length; i++)
    {
        if (actionSlots[i].GetActionSprite() == null)
        {
            return actionSlots[i];
        }
    }
    return null;
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
