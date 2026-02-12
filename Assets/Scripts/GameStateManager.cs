using System.Security;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string state;
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
               //Player.ActionQueue.dequeue().execute(Player)
               //GoblinWarrior.ActionQueue.dequeue().execute(GoblinWarrior)
               //GoblinArcher.ActionQueue.dequeue().execute(GoblinArcher)
            }
        }
    }

    void startActionPhase()
    {
        state = "action";
    }

    void endActionPhase()
    {
        state = "prep";
    }
}
