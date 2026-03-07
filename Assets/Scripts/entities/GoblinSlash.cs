using UnityEngine;
using System.Collections;
using Assets.Scripts.player_actions;

namespace Assets.Scripts
{
	public class GoblinSlash: MonoBehaviour
	{

        [SerializeField] private AudioSource slashSFX;
        [SerializeField] private AudioSource hitShieldSFX;

        public HealthBarControl healthBarControl;

        void Awake()
    {
        healthBarControl = FindFirstObjectByType<HealthBarControl>();

        if (healthBarControl == null)
        {
            Debug.LogError("HealthBarControl not found in scene!");
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.GetComponent<PlayerScript>();
            if (player != null)
            {
                if (player.isBlocking )
                {
                    hitShieldSFX.Play();
                    return;
                }
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