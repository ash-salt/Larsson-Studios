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
        if (completed)
        {
            setToCompleted();
        }
        if (IsUnlocked())
        {
            setToUnlocked();
        }
        else
        {
            setToLocked();
        }
        if (!completed && IsUnlocked()) {        
            StartCoroutine(SwapRoutine());
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
    }

    public void setToCompleted()
    {
        completed = true;
        setToUnlocked();
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
        if (prerequisite == null)
        {
            return true;
        }
        else
        {
            return prerequisite.isCompleted();
        }
    }

    public bool isCompleted()
    {
        return completed;
    }

    public void loadWorld()
    { 
        worldManager.setWorld(worldID);
        Application.LoadLevel(worldID);
    }

    public void OnMouseDown()
    {
        print("clicked");
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
