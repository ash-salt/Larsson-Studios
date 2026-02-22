using UnityEngine;
using System.Collections;
using Assets.Scripts.player_actions;

namespace Assets.Scripts
{
	public class GoblinSlash: MonoBehaviour
	{

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<PlayerScript>();
            if (player != null)
            {
                if (player.isBlocking )
                {
                    print("blocked nerd");
                    return;
                }
                print("goblin hits!");
                player.damage(25);
                var rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {

                }
            }
        }
    }
}