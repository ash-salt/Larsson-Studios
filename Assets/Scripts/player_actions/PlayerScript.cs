using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions
{
	public class PlayerScript: EntityScript
	{

		// Use this for initialization
		void Start()
		{
            GameStateManager.Instance.AddToEntityList(this);
        }

		// Update is called once per frame
		

		public void SetBlocking(Boolean isBlocking)
		{
			this.isBlocking = isBlocking;
		}


		public void QueueMove(Vector2 targetPos, float maxDistance = 3f)
		{
			EnqueueAction(new MoveAction(targetPos, maxDistance, GameStateManager.Instance.getUIPosition()));
		}
	
	}
}