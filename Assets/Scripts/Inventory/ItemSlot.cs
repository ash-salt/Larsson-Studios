using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Inventory inventory;
    [SerializeField] private Item item;
    [SerializeField] private Sprite defaultsprite;
    [SerializeField] private Image itemImage;
    public void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (item != null)
        {
            itemImage.sprite = item.GetItemSprite();
        }
        else
        {
            itemImage.sprite = defaultsprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Item slot clicked!");
        inventory.SelectItem(this);
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.GetItemSprite();
    }

    public void ClearItem()
    {
        item = null;
        itemImage.sprite = defaultsprite;
    }   
    public Item GetItem()
    {
        return item;
    }
}