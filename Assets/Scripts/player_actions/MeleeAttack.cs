using System.Linq;
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
    GameObject slashPrefab;
    Vector3 spawnPos;
    Quaternion rotation;
    float angle;

    public MeleeAttack(Quaternion rotation, Vector3 spawnPos)
    {
        this.slashPrefab = GameStateManager.Instance.GetSlashPrefab();
        this.spawnPos = spawnPos;
        this.rotation = rotation;
    }

    public int getCooldown()
    {
        return 0;
    }

    public int getCost()
    {
        return 1;
    }

    public void execute(EntityScript player)
    {
        slashInstance = GameObject.Instantiate(slashPrefab, spawnPos, rotation);
    }

    public void Dispose()
    {
        Object.Destroy(slashInstance);
    }
}
