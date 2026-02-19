using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int maxHealth;
    public int currentHealth;

    public float maxMoveDistance = 3f;
    Queue<IAction> actions = new Queue<IAction>();
    public bool done = false;
    public bool isDead = false;

    public bool isBlocking = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IAction DequeueAction()
    {
        try {
            IAction a = actions.Dequeue();
            return a;
        }
        catch (Exception e)
        {
            print("no action here");
            return null;
        }
    }

    public void EnqueueAction(IAction a)
    {
        actions.Enqueue(a);
    }

    public void doneWithAction()
	{
		done = true;
        print("we are done!!!!");
	}

    public void Die()
    {
        print("now we are in the Die() method");
        isDead = true;
    }
}
