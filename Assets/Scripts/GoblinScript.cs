using Assets.Scripts.player_actions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class GoblinScript: MonoBehaviour
	{

		GameObject player;

        // Use this for initialization
        void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void PlanTurn()
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        }

    }
}