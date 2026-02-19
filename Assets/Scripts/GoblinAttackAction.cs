using Assets.Scripts.player_actions;
using UnityEngine;

public class GoblinAttackAction : MonoBehaviour, IAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int damage;
    float spawnDistance = 0.75f;

    public void SlashAttack(EntityScript player, EntityScript goblin)
    {
        GameObject slashPrefab = GameStateManager.Instance.GetGoblinSlashPrefab();

        Vector3 goblinPos = goblin.transform.position;

        Vector2 direction = (player.transform.position - goblinPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Vector3 spawnPosition = goblinPos
                              + (Vector3)direction * spawnDistance;

        GameObject.Instantiate(slashPrefab, spawnPosition, rotation);
    }

    public int getCost()
    {
        return 1;
    }

    public void execute(EntityScript goblin)
    {
        EntityScript player = null;
        foreach (EntityScript character in GameStateManager.Instance.GetEnemyList())
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

