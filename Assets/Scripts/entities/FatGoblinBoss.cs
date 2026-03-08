using UnityEngine;

public class FatGoblinBoss : Assets.Scripts.GoblinScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool readyToSpawn = true;
    [SerializeField] private GameObject goblinPrefab;
    [SerializeField] private GameObject goblinArcherPrefab;
    public override void PlanTurn()
    {
        if (readyToSpawn)
        {
            EnqueueAction(ScriptableObject.CreateInstance<EmptyAction>());
            EnqueueAction(ScriptableObject.CreateInstance<EmptyAction>());
            EnqueueAction(new FatGoblinSummonAction(goblinPrefab, goblinArcherPrefab));
        }
        readyToSpawn = !readyToSpawn;
    }
}
