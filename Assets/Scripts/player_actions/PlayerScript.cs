using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions {
public class PlayerScript: EntityScript
	{
		private Item selectedItem;
		[SerializeField] public AAction attackAction;
		[SerializeField] public AAction utilAction;
		[SerializeField] public AAction moveAction;
		[SerializeField] public SpriteRenderer spriteRenderer;

		public AAction getAction(int index)
		{
			switch (index)
			{
				case 0:
					return moveAction;
				case 1:
					return attackAction;
				case 2:
					return utilAction;
			}
			return null;
		}

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
		public void QueueMove(MoveAction action)
		{
			EnqueueAction(action);
		}
	
	}
}