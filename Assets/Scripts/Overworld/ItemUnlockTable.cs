using Unityengine;

public class ItemUnlockTable : MonoBehaviour
{
    [SerializeField] private Dictionary<String, Item> unlockTable;


    public Item unlockItem(String worldID)
    {
        if (unlockTable.ContainsKey(worldID))
        {
            return unlockTable[worldID];
        }
        return null;
    }
}