using UnityEngine;
using System.Collections.Generic;


public class ItemUnlockTable : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Item> unlockTable;


    public Item unlockItem(string worldID)
    {
        if (unlockTable.ContainsKey(worldID))
        {
            return unlockTable[worldID];
        }
        return null;
    }
}