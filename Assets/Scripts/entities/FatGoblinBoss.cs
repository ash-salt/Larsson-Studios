using UnityEngine;

public class FatGoblinBoss : Assets.Scripts.GoblinScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool readyToSpawn = true;
    private void QueueSummonAction() {
        FatGoblinSummonAction summonAction = (FatGoblinSummonAction) attackActionData.createAction();
        EnqueueAction(summonAction);
    }

    public override void PlanTurn()
    {
        if (readyToSpawn)
        {
            Debug.Log("FatGoblinBoss is summoning!");
            QueueSummonAction();
        }
        readyToSpawn = !readyToSpawn;
    }
}
