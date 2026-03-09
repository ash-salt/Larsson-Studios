using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions {
public class PlayerScript: EntityScript
	{
		private Item selectedItem;
		[SerializeField] public ActionData attackAction;
		[SerializeField] public ActionData utilAction;
		[SerializeField] public ActionData moveAction;
		[SerializeField] public SpriteRenderer spriteRenderer;

		public ActionData getAction(int index)
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
				NotifyActionsChanged(); 
			}
        }
		public event Action OnActionsChanged;

		public void NotifyActionsChanged()
		{		
			OnActionsChanged?.Invoke();
		}
	
	}
}