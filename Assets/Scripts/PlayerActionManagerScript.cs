using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Queue<Action> ActionQueue = new Queue<Action>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToActionQueue(Action action)
    {
        ActionQueue.Enqueue(action);
    }
}
