using Assets.Scripts;
using UnityEngine;
using Assets.Scripts.player_actions;
public class GoblinArcherScript : GoblinScript
{
    private void QueueRangedAttack(bool prepared)
    {
        GoblinRangedAttack attack = (GoblinRangedAttack) attackActionData.createAction();
        attack.Initialize(prepared, player.transform.position);
        EnqueueAction(attack);
    }

    public override void PlanTurn()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized * -1;
        Vector3 targetPosition = direction * maxMoveDistance;

        if (distanceToPlayer < attackDistance)
        {
            QueueMove(targetPosition);
            QueueRangedAttack(false);
            QueueRangedAttack(true);
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer < shortDistance)
        {
            QueueMove(targetPosition);
            QueueRangedAttack(false);
            QueueRangedAttack(true);
        }
        else if (distanceToPlayer > shortDistance && distanceToPlayer < mediumDistance)
        {
            QueueRangedAttack(false);
            QueueRangedAttack(true);
            QueueMove(targetPosition);
        }
        else if (distanceToPlayer > mediumDistance && distanceToPlayer < farAway)
        {
            QueueRangedAttack(false);
            QueueRangedAttack(true);
        }
    }
}