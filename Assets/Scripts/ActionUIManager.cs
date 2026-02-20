using UnityEngine;

public class ActionUIManager : MonoBehaviour
{
    public GameObject Actions;
    public ActionSlot[] actionSlots;
    [SerializeField] private EntityScript playerScript;
    [SerializeField] private Sprite blockSprite;

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
    

    public void clearActionUI()
    {
        foreach (var slot in actionSlots)
        {
            slot.ClearActionSprite();
        }
    }
}
