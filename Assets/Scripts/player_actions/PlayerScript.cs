using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions
{
	public class PlayerScript: EntityScript
	{
		Boolean isBlocking = false;


		// Use this for initialization
		void Start()
		{
            GameStateManager.Instance.AddToEntityList(this);
        }

		// Update is called once per frame
		void Update()
		{

		}

		public void SetBlocking(Boolean isBlocking)
		{
			this.isBlocking = isBlocking;
		}

		public void damage(int amount)
		{
			this.currentHealth -= amount;
			if (this.currentHealth <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			
		}
	}
}