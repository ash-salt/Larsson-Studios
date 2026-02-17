using Assets.Scripts.player_actions;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Assets.Scripts
{
	public class GoblinScript: EntityScript
	{

		GameObject player;
		int farAway = 10;
		int mediumDistance = 6;
		int shortDistance = 3;
		int closeDistance = 1;

        // Use this for initialization
        void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void PlanTurn()
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
			EnqueueAction(null);

        }

		public void damage(int amount)
		{
			this.currentHealth -= amount;
			if (currentHealth <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			
		}

    }
}