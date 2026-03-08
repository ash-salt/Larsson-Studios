using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
namespace Assets.Scripts.player_actions {
public class BlockAction: AAction
	{
		public GameObject shieldInstance;

		public BlockAction(ActionData actionData)
		{
			this.actionData = actionData;
		}

		public override void execute()
		{
			GameObject shieldPrefab = GameStateManager.Instance.GetShieldPrefab();
            target.SetBlocking(true);
			Vector3 playerPos = target.transform.position;
			Quaternion rotation = Quaternion.Euler(0, 0, 0);
			shieldInstance = GameObject.Instantiate(shieldPrefab, playerPos, rotation);
        }

		public void Dispose()
		{
			Object.Destroy(shieldInstance);
		}
	
	}
}