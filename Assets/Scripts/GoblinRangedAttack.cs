using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinRangedAttack : IAction 
{
    private bool prepared;
    public PlayerScript player;
    private LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
    private LayerMask playerLayer = LayerMask.GetMask("Ignore Raycast");

    public LineRenderer lineRenderer = new LineRenderer();

    public GoblinRangedAttack(bool isPrepared)
    {
        prepared = isPrepared;
    }
    public int getCost()
    {
        return 1;
    }

    private Vector2 playerPos;

    public void execute(EntityScript entity)
    {
        Debug.Log("prepared is " + prepared);
        if (prepared == false)
        {
            MonoBehaviour.print("here we go");
            foreach (EntityScript e in GameStateManager.Instance.GetEntityList())
            {
                if (e is PlayerScript p)
                {
                    player = p;
                }
            }
            playerPos = player.transform.position;
            prepared = true;
        }
        else
        {
            MonoBehaviour.print("firing!");
            Vector2 origin = entity.transform.position;
            Vector2 direction = (playerPos - origin).normalized;
            origin = origin + direction*0.5f;
            float distance = Vector2.Distance(origin, playerPos);

            RaycastHit2D hit = Physics2D.Raycast(
                origin,
                direction,
                distance,
                obstacleLayer
            );

            if (hit.collider != null)
            {
                // Hit wall/obstacle
                Debug.Log("hit object");
                li
            }
            else
            {
                // No obstacle → check player
                RaycastHit2D entityHit = Physics2D.Raycast(
                    origin,
                    direction,
                    distance,
                    playerLayer
                );

                if (entityHit.collider.GetComponent<EntityScript>() != null)
                {
                    entityHit.collider.GetComponent<EntityScript>().damage(25);
                    Debug.Log("hit something with " + entityHit.collider.GetComponent<EntityScript>().maxHealth + " hp, now has " + entityHit.collider.GetComponent<EntityScript>().currentHealth);
                }
            }

            prepared = false;
        }
    }
}
