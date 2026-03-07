using Assets.Scripts;
using UnityEngine;
using Assets.Scripts.player_actions;

public class GoblinArcherScript : GoblinScript
{
    [SerializeField] GoblinRangedAttack RangedAttack;
    public override void PlanTurn()
    {   
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); 

        Vector3 direction = (player.transform.position - transform.position).normalized * -1;
        Vector3 targetPosition = direction*maxMoveDistance;


        if (distanceToPlayer < attackDistance)
        {
            QueueMove(targetPosition);
            RangedAttack.Initialize(false, player.transform.position);
            EnqueueAction(RangedAttack);
            RangedAttack.Initialize(true, player.transform.position);
            EnqueueAction(RangedAttack);
        }
        else if (shortDistance > distanceToPlayer && distanceToPlayer > attackDistance)
        {
            QueueMove(targetPosition);
            RangedAttack.Initialize(false, player.transform.position);
            EnqueueAction(RangedAttack);
            RangedAttack.Initialize(true, player.transform.position);
            EnqueueAction(RangedAttack);
        }
        else if (mediumDistance > distanceToPlayer && distanceToPlayer > shortDistance)
        {
            RangedAttack.Initialize(false, player.transform.position);
            EnqueueAction(RangedAttack);
            RangedAttack.Initialize(true, player.transform.position);
            EnqueueAction(RangedAttack);
            QueueMove(targetPosition);
        }
        else if (farAway > distanceToPlayer && distanceToPlayer > mediumDistance)
        {
            RangedAttack.Initialize(false, player.transform.position);
            EnqueueAction(RangedAttack);
            RangedAttack.Initialize(true, player.transform.position);
            EnqueueAction(RangedAttack);
        }
        
    }


}
