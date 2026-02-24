using UnityEngine;
using System.Collections;
using Assets.Scripts.player_actions;

namespace Assets.Scripts
{
	public class GoblinSlash: MonoBehaviour
	{

        [SerializeField] private AudioSource slashSFX;
        [SerializeField] private AudioSource hitShieldSFX;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<PlayerScript>();
            if (player != null)
            {
                if (player.isBlocking )
                {
                    print("blocked nerd");
                    hitShieldSFX.Play();
                    return;
                }
                print("goblin hits!");
                slashSFX.Play();
                player.damage(25);
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    //rb.AddForce()
                }
            }
        }
    }
}