using UnityEngine;
using Assets.Scripts.player_actions;
using System;

public class Trap : MonoBehaviour
{
    private bool enabled;
    private AudioSource audioSource;
    [SerializeField] private SpriteRenderer sprite;
    public void Start()
    {
        enabled = true;
        audioSource = GetComponent<AudioSource>();
    }

    public void changeSprite(Sprite newSprite) {
        sprite.sprite = newSprite;
    }

    public void move(Vector3 pos) {
        this.transform.position = pos;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        player.ClearActions();
        if (player != null && enabled)
        {
            Debug.Log("Trap triggered!");
            //audioSource.Play();
            NotifyTrapTrigger();
        }
    }

    public event Action OnTrapTrigger;

	public void NotifyTrapTrigger()
		{		
			OnTrapTrigger?.Invoke();
		}
}
