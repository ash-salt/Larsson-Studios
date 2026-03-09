using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.player_actions;
using TMPro;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance { get; private set; }
    private PlayerScript player;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Image playersprite;
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private ItemSlot selectedItem;
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SelectItem(ItemSlot item)
    {
        Debug.Log("Item selected!");
        Item itemeffect = item.GetItem();
        if (itemeffect == null)
        {
            return;
        }
        Debug.Log("2");
        selectedItem.SetItem(itemeffect);
        updateDescriptionText(itemeffect);
        updateCharacterImage(itemeffect);
        WorldManager.Instance.item = itemeffect;
    }

    public void updateDescriptionText(Item item)
    {
        descriptionText.gameObject.SetActive(true);
        descriptionText.text = item.GetItemName() + ": " + item.GetDescription();
    }
    public void updateCharacterImage(Item item)
    {
        if (item is Characterable itim) {
            playersprite.sprite = itim.GetSprite();
        }
        else {
            playersprite.sprite = defaultSprite;
        }
    }
}