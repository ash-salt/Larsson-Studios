using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinAttackAction : MonoBehaviour, IAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCost()
    {
        return 1;
    }

    public void execute(GameObject target)
    {
        if (target.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            player.damage(25);
        }
    }
}
