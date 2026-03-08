using Assets.Scripts.player_actions;
using UnityEngine;

public class FatGoblinSummonAction : AAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject goblinPrefab;
    private GameObject goblinArcherPrefab;

    public FatGoblinSummonAction(ActionData actionData)
    {
        this.actionData = actionData;
        FatGoblinSummonData summonData = (FatGoblinSummonData) actionData;
        this.goblinPrefab = summonData.goblinPrefab;
        this.goblinArcherPrefab = summonData.goblinArcherPrefab;
    }

    public override void execute()
    {
        Debug.Log("goblinPrefab is " + (goblinPrefab == null ? "NULL" : goblinPrefab.name));
        Debug.Log("goblinArcherPrefab is " + (goblinArcherPrefab == null ? "NULL" : goblinArcherPrefab.name));
    
        int which = Random.Range(0,2);
        float x = Random.Range(-2.6f, 2.7f);
        float y = Random.Range(-2.4f, 1.6f);
        Vector3 spawnPosition = new Vector3(x, y, 0f);
        Debug.Log("Spawning " + (which == 0 ? "goblin" : "archer") + " at " + spawnPosition);
        if (which == 0)
        {
            UnityEngine.Object.Instantiate(goblinPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            UnityEngine.Object.Instantiate(goblinArcherPrefab, spawnPosition, Quaternion.identity);
        }
        target.doneWithAction();
        
    }
}
