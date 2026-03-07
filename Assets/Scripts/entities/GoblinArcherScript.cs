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
            EnqueueAction(new GoblinRangedAttack(false, player.transform.position));
            EnqueueAction(new GoblinRangedAttack(true, player.transform.position));
        }
        else if (shortDistance > distanceToPlayer && distanceToPlayer > attackDistance)
        {
            QueueMove(targetPosition);
            EnqueueAction(new GoblinRangedAttack(false, player.transform.position));
            EnqueueAction(new GoblinRangedAttack(true, player.transform.position));
        }
        else if (mediumDistance > distanceToPlayer && distanceToPlayer > shortDistance)
        {
            EnqueueAction(new GoblinRangedAttack(false, player.transform.position));
            EnqueueAction(new GoblinRangedAttack(true, player.transform.position));
            QueueMove(targetPosition);
        }
        else if (farAway > distanceToPlayer && distanceToPlayer > mediumDistance)
        {
            EnqueueAction(new GoblinRangedAttack(false, player.transform.position));
            EnqueueAction(new GoblinRangedAttack(true, player.transform.position));
        }
        
    }


}
