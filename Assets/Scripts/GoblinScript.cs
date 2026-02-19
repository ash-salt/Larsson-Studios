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
		float closeDistance = 0.5f;

        // Use this for initialization
        void Start()
		{
            GameStateManager.Instance.AddToEnemyList(this);
			GameStateManager.Instance.AddToEntityList(this);

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
				print("goblin is dead ez ez ez");
				Die();
			}
		}


    }
}