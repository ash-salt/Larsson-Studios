using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
namespace Assets.Scripts.player_actions {
	[CreateAssetMenu(menuName = "Scriptable Objects/Block")]

public class BlockAction: AAction
	{
		public GameObject shieldInstance;

		public void OnEnable()
		{
        	buttonInstruction = new BlockButtonScript(this);
			cooldown = 1;
		}

		public void Initialize()
		{
			return;
		}

		public override void CopyFrom(AAction source)
        {
            if (source is BlockAction src)
			{
				this.shieldInstance = src.shieldInstance;
			}
        }

		public ButtonInstruction getInstructions()
        {
            return buttonInstruction;
        }

		public override void execute(EntityScript target)
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