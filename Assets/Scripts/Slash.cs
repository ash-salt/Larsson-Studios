using UnityEngine;

public class Slash : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit!");
        var goblin = collision.GetComponent<Assets.Scripts.GoblinScript>();
        if (goblin != null)
        {
            goblin.damage(50);
        }
    }
}
