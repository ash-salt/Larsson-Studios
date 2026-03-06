using UnityEngine;
using Assets.Scripts.player_actions;
public abstract class Item : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public string description;

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetItemSprite() {
        return itemSprite;
    }
    public string GetDescription()
    {
        return description;
    }

    public abstract void UseItem(PlayerScript player);
}