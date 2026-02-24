using UnityEngine;

public class Slash : MonoBehaviour
{
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var goblin = collision.GetComponent<Assets.Scripts.GoblinScript>();
        if (goblin != null)
        {
            print("hit!");
            goblin.damage(25);
            var rb = goblin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
            }
        }
    }
}
