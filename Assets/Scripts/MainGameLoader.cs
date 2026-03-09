using UnityEngine;
using System.Collections;

public class MainGameLoader: MonoBehaviour {
    public void LoadMainGame()
    { 
        Application.LoadLevel("Overworld");
    }

    public void LoadTutorial()
    {
        Application.LoadLevel("Tutorial");
    }

    public void LoadInventory()
    {
        Application.LoadLevel("Inventory");
    }
}
