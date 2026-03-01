using UnityEngine;
using System.Collections;

public class MainGameLoader: MonoBehaviour {
    public void LoadMainGame()
    { 
        Application.LoadLevel("Overworld");
    }
}
