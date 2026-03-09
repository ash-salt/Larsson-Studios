using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class World: MonoBehaviour {

    [SerializeField] string worldID;

    [SerializeField] World prerequisite;

    [SerializeField] Sprite blinkingSprite;

    [SerializeField] Sprite lockedSprite;

    [SerializeField] Sprite unlockedSprite;

    [SerializeField] SpriteRenderer completedSprite;

    [SerializeField] private SpriteRenderer currentSprite;

    [SerializeField] private Item[] unlockables;

    private WorldManager worldManager;

    private bool completed;

    void Start()
    {           
        worldManager = WorldManager.Instance;
        completed = worldManager.isCompleted(worldID);
        Debug.Log(completed);
        if (completed)
        {
            setToCompleted();
        }
        else if (IsUnlocked())
        {
            setToUnlocked();
        }
        else
        {
            setToLocked();
        }
    }

    IEnumerator SwapRoutine()
    {
        while (true)
        {
            swapSprite();
            yield return new WaitForSeconds(1f);
        }
    }

    public string getworldID()
    {
        return worldID;
    }

    public void setToLocked()
    {
        currentSprite.sprite = lockedSprite;
    }

    public void setToUnlocked()
    {
        currentSprite.sprite = unlockedSprite;
        StartCoroutine(SwapRoutine());
    }

    public void setToCompleted()
    {
        completed = true;
        completedSprite.enabled = true;
    }

    public void swapSprite()
    {
        if (currentSprite.sprite == blinkingSprite)
         {
             currentSprite.sprite = unlockedSprite;
         }
         else
         {
             currentSprite.sprite = blinkingSprite;
         }
    }
    public bool IsUnlocked()
    {
        Debug.Log("check");
        if (prerequisite == null)
        {
            Debug.Log(worldID + "check2");
            return true;
        }
        else
        {
            Debug.Log(worldID + "check3");
            
            Debug.Log(prerequisite.isCompleted());
            
            return prerequisite.isCompleted();
        }
    }

    public bool isCompleted()
    {
        return WorldManager.Instance.isCompleted(worldID);
    }

    public void loadWorld()
    { 
        worldManager.setWorld(worldID);
        Application.LoadLevel(worldID);
    }

    public void OnMouseDown()
    {
        print("clicked");
        Debug.Log(worldID);
        if (IsUnlocked() && !isCompleted())
        {
            loadWorld();
        }
        else
        {
            return;
        }
    }
}
