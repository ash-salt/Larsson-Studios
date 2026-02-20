using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

namespace Assets.Scripts.player_actions
{
	public class BlockAction: IAction
	{


		public int getCost()
		{
			return 1;
		}

		public void execute(EntityScript target)
		{
            if (target.TryGetComponent<PlayerScript>(out PlayerScript player))
            {
                player.SetBlocking(true);
            }

        }
	
	}
}