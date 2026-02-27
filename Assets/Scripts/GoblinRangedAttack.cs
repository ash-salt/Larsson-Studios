using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinRangedAttack : IAction 
{
    private bool prepared = false;
    public GameObject player;
    private LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
    private LayerMask playerLayer = LayerMask.GetMask("Ignore Raycast");
    public int getCost()
    {
        return 1;
    }

    public void execute(EntityScript entity)
    {
        if (prepared == false)
        {
            prepared = true;
        }
        else
        {
            Vector2 origin = entity.transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 direction = (playerPos - origin).normalized;
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
                return;
            }
            else
            {
                // No obstacle → check player
                RaycastHit2D playerHit = Physics2D.Raycast(
                    origin,
                    direction,
                    distance,
                    playerLayer
                );

                if (playerHit.collider != null)
                {
                    if (playerHit.collider is PlayerScript)
                    {
                        
                    }
                }
            }

            //currentState = EnemyState.Idle;
        }
    }
}
