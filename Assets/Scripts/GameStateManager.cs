using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string state;
    private List<EntityScript> gameEntities = new List<EntityScript>();
    void Start()
    {
        state = "prep";

    }

    // Update is called once per frame
    void Update()
    {
        if (state == "prep")
        {
            
        }
        else if (state == "action")
        {
            for (int i = 0; i < 3; i++)
            {
               executeActions();
            }
        }
    }

    public void startActionPhase()
    {
        state = "action";
    }

    public void endActionPhase()
    {
        state = "prep";
        foreach (EntityScript entity in gameEntities)
        {
        }
    }

    void executeActions()
    {
        foreach (EntityScript entity in gameEntities)
        {
            //entity.DequeueAction().execute(entity);
        }
    }
}
