using UnityEngine;

public class Slash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var goblin = collision.GetComponent<Assets.Scripts.GoblinScript>();
        if (goblin != null)
        {
            print("hit!");
            goblin.damage(50);
            var rb = goblin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
            }
        }
    }
}
