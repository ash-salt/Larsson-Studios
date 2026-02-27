using Assets.Scripts.player_actions;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Assets.Scripts
{
	public class GoblinScript: EntityScript
	{

        [SerializeField] public GameObject player;
        float farAway = 5f;
		float mediumDistance = 4f;
		float shortDistance = 2f;
		float attackDistance = 0.5f;

        // Use this for initialization
        void Start()
		{
            GameStateManager.Instance.AddToEnemyList(this);
			GameStateManager.Instance.AddToEntityList(this);

        }

		public void PlanTurn()
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
			float stopDistance = 0.3f;
			float randomRadius = 1f;  

			Vector3 direction = (player.transform.position - transform.position).normalized;
			Vector3 targetPosition = player.transform.position - direction * stopDistance;

			Vector2 randomCircle = Random.insideUnitCircle * randomRadius;

			Vector3 randomOffset = new Vector2(randomCircle.x,randomCircle.y);

			Vector3 finalTarget = targetPosition + randomOffset;

			if (distanceToPlayer < attackDistance)
			{
				EnqueueAction(new GoblinAttackAction());
			}
			else if (shortDistance > distanceToPlayer && distanceToPlayer > attackDistance)
			{
				QueueMove(finalTarget);
                EnqueueAction(new GoblinAttackAction());
            }
            else if (mediumDistance > distanceToPlayer && distanceToPlayer > shortDistance)
			{
                QueueMove(finalTarget);
                QueueMove(finalTarget);
                EnqueueAction(new GoblinAttackAction());
            }
            else if (farAway > distanceToPlayer && distanceToPlayer > mediumDistance)
            {
                QueueMove(finalTarget);
                QueueMove(finalTarget);
                QueueMove(finalTarget);
            }

        }

		public void takeDamage(int damage) {
        if (isBlocking)
        {
            print("Attack Blocked!");
            return;
        }
        else
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

        public void QueueMove(Vector2 targetPos, float maxDistance = 2f)
        {
            EnqueueAction(new MoveAction(targetPos, maxDistance, this.transform.position));
        }


    }
}