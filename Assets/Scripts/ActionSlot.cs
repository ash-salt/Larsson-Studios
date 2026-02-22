using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    private Sprite actionSprite;

    [SerializeField] public Image actionImage;

    public void SetActionSprite(Sprite newSprite)
    {
        actionSprite = newSprite;
        actionImage.sprite = actionSprite;
    }
    public void ClearActionSprite()
    {
        actionSprite = null;
        actionImage.sprite = null;
    }

    public Sprite GetActionSprite()
    {
        return actionSprite;
    }
}
