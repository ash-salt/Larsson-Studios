using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    private List<string> completedWorlds = new List<string>();

    private string worldID = "tutorial";

    //temporary references
    public Item item;

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

    public void CompleteWorld(string worldID)
    {
        if (!completedWorlds.Contains(worldID))
        {
            completedWorlds.Add(worldID);
        }
    }

    public bool isCompleted(string worldID)
    {
        return completedWorlds.Contains(worldID);
    }

    public void setWorld(string worldID)
    {
        this.worldID = worldID;
    }

    public void victory()
    {
        CompleteWorld(worldID);
        SceneManager.LoadScene("Overworld");
        
    }

    public void defeat()
    {
        SceneManager.LoadScene("MainMenu");
    }
}