using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinAttackAction : IAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int damage;
    float spawnDistance = 0.5f;

    public void SlashAttack(EntityScript playerChar, EntityScript goblin)
    {
        GameObject slashPrefab = GameStateManager.Instance.GetGoblinSlashPrefab();

        Vector3 playerPos = goblin.transform.position;

        Vector2 direction = (playerChar.transform.position - playerPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Vector3 spawnPosition = playerPos
                              + (Vector3)direction * spawnDistance;

        GameObject.Instantiate(slashPrefab, spawnPosition, rotation);
    }

    public int getCost()
    {
        return 1;
    }

    public void execute(EntityScript goblin)
    {
        MonoBehaviour.print("goblin is attacking!");
        EntityScript player = null;
        foreach (EntityScript character in GameStateManager.Instance.GetEntityList())
        {
            if (character is PlayerScript)
            {
                player = character;
            }

        }
        if (player != null)
        {
            SlashAttack(player, goblin);
            goblin.doneWithAction();
            MonoBehaviour.print("Done with attack trust");
        }
        
    }
}

