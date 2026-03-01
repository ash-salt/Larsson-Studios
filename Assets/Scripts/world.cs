using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class World: MonoBehaviour {

    [SerializeField] string worldID;

    [SerializeField] World prerequisite;

    [SerializeField] Sprite lockedSprite;

    [SerializeField] Sprite unlockedSprite;

    [SerializeField] Sprite completedSprite;

    [SerializeField] private Image lockSprite;

    [SerializeField] private Image currentSprite;

    private WorldManager worldManager;

    private bool completed;

    void Start()
    {   
        worldManager = WorldManager.Instance;
        if (worldManager.isCompleted(worldID))
        {
            setToCompleted();
        }
        if (IsUnlocked())
        {
            unlock();
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

    public void unlock()
    {
        lockSprite.enabled = false;

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
        currentSprite.sprite = completedSprite;
    }

    public void swapSprite()
    {
        if (currentSprite.sprite == lockedSprite)
        {
            setToUnlocked();
        }
        else
        {
            setToLocked();
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
            return;
        }
        else
        {
            return;
        }
    }
}
