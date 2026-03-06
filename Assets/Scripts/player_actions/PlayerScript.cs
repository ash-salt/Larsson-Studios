using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions
{
	public class PlayerScript: EntityScript
	{
		private Item selectedItem;
		private IAction attackAction;
		private IAction utilAction;
		private IAction moveAction;

		void Start()
		{
            GameStateManager.Instance.AddToEntityList(this);
			selectedItem = WorldManager.Instance.item;
			if (selectedItem != null)
			{
				selectedItem.UseItem(this);
			}
        }
		

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