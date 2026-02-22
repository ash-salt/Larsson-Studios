using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MeleeAttack :  IAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int damage;
    float spawnDistance = 0.75f;
    GameObject slashInstance;

    public void SlashAttack(EntityScript nearestGoblin, EntityScript player)
    {
        GameObject slashPrefab = GameStateManager.Instance.GetSlashPrefab();

        Vector3 playerPos = player.transform.position;

        Vector2 direction = (nearestGoblin.transform.position - playerPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Vector3 spawnPosition = playerPos
                              + (Vector3)direction * spawnDistance;

        slashInstance = GameObject.Instantiate(slashPrefab, spawnPosition, rotation);
    }

    public int getCost()
    {
        return 1;
    }

    public void execute(EntityScript player)
    {
        EntityScript minDistance = GameStateManager.Instance.GetEnemyList()[0];
        if (minDistance != null)
        {
            foreach (EntityScript enemy in GameStateManager.Instance.GetEnemyList())
            {
                if (Vector3.Distance(player.transform.position, enemy.transform.position) < (Vector3.Distance(player.transform.position, minDistance.transform.position)))
                {
                    minDistance = enemy;
                }

            }
            SlashAttack(minDistance, player);
            player.doneWithAction();
            MonoBehaviour.print("Done with attack trust");
        }
    }

    public void Dispose()
    {
        Object.Destroy(slashInstance);
    }


}
