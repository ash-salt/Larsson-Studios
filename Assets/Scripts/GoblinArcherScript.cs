using Assets.Scripts;
using UnityEngine;

public class GoblinArcherScript : GoblinScript
{
    public override void PlanTurn()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); 

        Vector3 direction = (player.transform.position - transform.position).normalized * -1;
        Vector3 targetPosition = direction*maxMoveDistance;


        if (distanceToPlayer < attackDistance)
        {
            QueueMove(targetPosition);
            EnqueueAction(new GoblinRangedAttack(false));
            EnqueueAction(new GoblinRangedAttack(true));
        }
        else if (shortDistance > distanceToPlayer && distanceToPlayer > attackDistance)
        {
            QueueMove(targetPosition);
            EnqueueAction(new GoblinRangedAttack(false));
            EnqueueAction(new GoblinRangedAttack(true));
        }
        else if (mediumDistance > distanceToPlayer && distanceToPlayer > shortDistance)
        {
            EnqueueAction(new GoblinRangedAttack(false));
            EnqueueAction(new GoblinRangedAttack(true));
            QueueMove(targetPosition);
        }
        else if (farAway > distanceToPlayer && distanceToPlayer > mediumDistance)
        {
            EnqueueAction(new GoblinRangedAttack(false));
            EnqueueAction(new GoblinRangedAttack(true));
        }
        
    }


}
