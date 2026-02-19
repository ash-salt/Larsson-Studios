using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class GoblinSlash: MonoBehaviour
	{

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print("goblin hits!");
            var player = collision.GetComponent<player_actions.PlayerScript>();
            if (player != null)
            {
                player.damage(25);
                var rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {

                }
            }
        }
    }
}