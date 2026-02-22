using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    private Sprite actionSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] public Image actionImage;

    public void Awake()
    {
        ClearActionSprite();
    }
    public void SetActionSprite(Sprite newSprite)
    {
        actionSprite = newSprite;
        actionImage.sprite = newSprite;
    }
    public void ClearActionSprite()
    {
        actionSprite = null;
        actionImage.sprite = defaultSprite;
    }

    public Sprite GetActionSprite()
    {
        return actionSprite;
    }
}
