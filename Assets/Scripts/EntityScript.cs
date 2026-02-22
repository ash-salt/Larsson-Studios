using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int maxHealth;
    public int currentHealth;
    public bool done = false;
    public bool isDead = false;
    public bool isBlocking = false;

    public float maxMoveDistance = 3f;
    Queue<IAction> actions = new Queue<IAction>();


    public IAction[] getActions() {
        return actions.ToArray();
    }

    public void ClearActions() {
        actions.Clear();
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
        if (actions.Count < 4) {
            actions.Enqueue(a);
        }
    }

    public void takeDamage(int damage)
    {
        if (isBlocking)
        {
            print("Attack Blocked!");
            return;
        }
        else
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
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
