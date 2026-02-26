using Assets.Scripts;
using UnityEngine;

public class GoblinArcherScript : GoblinScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PlanTurn()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
    }


}
