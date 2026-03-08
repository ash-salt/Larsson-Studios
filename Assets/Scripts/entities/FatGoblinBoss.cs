using UnityEngine;

public class FatGoblinBoss : Assets.Scripts.GoblinScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool readyToSpawn = true;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite castingSprite;
    private void QueueSummonAction() {
        FatGoblinSummonAction summonAction = (FatGoblinSummonAction) attackActionData.createAction();
        EnqueueAction(summonAction);
    }

    public override void PlanTurn()
    {
        if (readyToSpawn)
        {
            Debug.Log("FatGoblinBoss is summoning!");
            gameObject.GetComponent<SpriteRenderer>().sprite = castingSprite;
            QueueSummonAction();
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
        }
        readyToSpawn = !readyToSpawn;
    }

    public void idle()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
    }
}
