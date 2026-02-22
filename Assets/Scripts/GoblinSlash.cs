using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
	public class GoblinSlash: MonoBehaviour
	{

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<player_actions.PlayerScript>();
            if (player != null)
            {
                print("goblin hits!");
                player.takeDamage(25);
                var rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {

                }
            }
        }
    }
}