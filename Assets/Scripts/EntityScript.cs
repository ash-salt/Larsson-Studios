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
    LinkedList<IAction> actions = new LinkedList<IAction>();

    public void ClearActions() {
        actions.Clear();
    }

    public IAction lastAction() {
        if (actions.Count > 0) {
            IAction last = actions.Last.Value;
            actions.RemoveLast();
            return last;
        }
        else {
            return null;
        }
    }

    public IAction DequeueAction()
    {
        try {
            IAction a = actions.First.Value;
            actions.RemoveFirst();
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
        int total_cost = 0;
        foreach (IAction action in actions)
        {
            total_cost += action.getCost();
        }
        if (total_cost + a.getCost() <= 3)
        {
            actions.AddLast(a);
        }
    }

    public void damage(int damage)
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

    public virtual void Die()
    {
        print("now we are in the Die() method");
        isDead = true;
    }

}
