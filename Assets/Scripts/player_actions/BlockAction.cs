using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

namespace Assets.Scripts.player_actions
{
	public class BlockAction: MonoBehaviour, IAction
	{

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

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