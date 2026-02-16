using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.player_actions
{
	public class PlayerScript: EntityScript
	{
		Boolean isBlocking = false;


		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void SetBlocking(Boolean isBlocking)
		{
			this.isBlocking = isBlocking;
		}
	}
}