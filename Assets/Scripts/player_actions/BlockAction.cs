using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

namespace Assets.Scripts.player_actions
{
	public class BlockAction: IAction
	{
		GameObject shieldInstance;
		public int getCost()
		{
			return 1;
		}

		public void execute(EntityScript target)
		{
			GameObject shieldPrefab = GameStateManager.Instance.GetShieldPrefab();
            if (target.TryGetComponent<PlayerScript>(out PlayerScript player))
            {
                player.SetBlocking(true);
				Vector3 playerPos = player.transform.position;
				Quaternion rotation = Quaternion.Euler(0, 0, 0);
				shieldInstance = GameObject.Instantiate(shieldPrefab, playerPos, rotation);
            }
        }

		public void Dispose()
		{
			Object.Destroy(shieldInstance);
		}
	
	}
}