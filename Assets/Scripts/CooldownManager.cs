using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.player_actions;
using System;

public class CooldownManager : MonoBehaviour
{
    private Dictionary<Type, int> cooldownTracker = new Dictionary<Type, int>();
    [SerializeField] public GenericButton[] buttons;

    public CooldownManager()
    {
        
    }

    public void Start()
    {
        return;
        findButton();
    }

    public void AddButton(GenericButton button)
    {
        return;
        //buttons.Add(button);
    }

    public void findButton()
    {
        return;
        //buttons.AddRange(FindObjectsOfType<GenericButton>());   
    }


    public void addCooldown(AAction action)
    {
        if (action.getCooldown() == 0) return;
        cooldownTracker[action.GetType()] = action.getCooldown();
        notifyButtons(action);

    }

    public void tickCooldowns()
    {
        List<Type> keys = new List<Type>(cooldownTracker.Keys);
        foreach (Type action in keys)
        {
            
            cooldownTracker[action]--;
            if (cooldownTracker[action] <= 0)
            {
                cooldownTracker.Remove(action);
            }
            notifyButtons(action);
        }
    }

    public bool onCooldown(AAction action)
    {
        return cooldownTracker.ContainsKey(action.GetType());
    }
    public bool onCooldown(Type action)
    {
        return cooldownTracker.ContainsKey(action);
    }

    public void removeCooldown(AAction action)
    {
        cooldownTracker.Remove(action.GetType());
        notifyButtons(action);
    }

    public void notifyButtons(AAction action)
    {
        Debug.Log("Notify1");
        foreach (GenericButton button in buttons)
        {
            button.SetCooldown(action.GetType(), onCooldown(action));
        }
    }

    public void notifyButtons(Type action)
    {
        Debug.Log("Notify");
        foreach (GenericButton button in buttons)
        {
            button.SetCooldown(action, onCooldown(action));
        }
    }
}