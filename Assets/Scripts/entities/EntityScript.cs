using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assets.Scripts.player_actions;

public class EntityScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int maxHealth;
    public int currentHealth;
    public bool done = false;
    public bool isDead = false;
    public bool isBlocking = false;

    public HealthBarControl healthBarControl;
    public float maxMoveDistance = 3f;
    public LinkedList<AAction> actions = new LinkedList<AAction>();

    public void ClearActions() {
        actions.Clear();
    }

    public bool fullActionQueue() {
        int total_cost = 0;
        foreach (AAction action in actions)
        {
            total_cost += action.getCost();
        }
        return total_cost >= 3;
    }

    public void Awake()
    {
        healthBarControl = FindFirstObjectByType<HealthBarControl>();
		if (healthBarControl == null)
			{
				Debug.LogError("HealthBarControl not found in scene!");
			}
    }

    public AAction lastAction() {
        if (actions.Count > 0) {
            AAction last = actions.Last.Value;
            actions.RemoveLast();
            return last;
        }
        else {
            return null;
        }
    }

    public AAction DequeueAction()
    {
        try {
            AAction a = actions.First.Value;
            actions.RemoveFirst();
            return a;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public void EnqueueAction(AAction template)
    {
        int total_cost = 0;
        foreach (AAction action in actions)
        {
            total_cost += action.getCost();
        }

        if (total_cost + template.getCost() <= 3)
        {
            // clone the ScriptableObject to make a unique instance
            AAction actionInstance = Instantiate(template);
            actionInstance.CopyFrom(template);
            actions.AddLast(actionInstance);
        }
    }

    public void damage(int damage)
    {
        if (isBlocking)
        {
            return;
        }
        else
        {
            currentHealth -= damage;
            healthBarControl.HealthChanged();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void doneWithAction()
	{
		done = true;
	}

    public virtual void Die()
    {
        isDead = true;
    }

}
