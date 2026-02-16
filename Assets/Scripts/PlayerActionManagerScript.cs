using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManagerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerScript playerScript;
    IAction currentAction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAction(IAction action)
    {
        currentAction = action;
    }

    public void AddToActionQueue(Vector3 tilePosition)
    {

        //IAction action = currentAction;
        //action.SetTarget(tilePosition);

        playerScript.Enqueue(action);
    }
}
