using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int maxHealth;
    int currentHealth;
    Queue<IAction> actions = new Queue<IAction>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IAction DequeueAction()
    {
        IAction a = actions.Dequeue();
        return a;
    }

    public void EnqueueAction(IAction a)
    {
        actions.Enqueue(a);
    }
}
