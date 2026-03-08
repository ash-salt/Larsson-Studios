using Assets.Scripts.player_actions;
using UnityEngine;

public class FatGoblinSummonAction : AAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject goblinPrefab;
    private GameObject goblinArcherPrefab;
    public FatGoblinSummonAction(GameObject goblinPrefab, GameObject goblinArcherPrefab)
    {
        this.goblinPrefab = goblinPrefab;
        this.goblinArcherPrefab = goblinArcherPrefab;
    }
    public void Init(GameObject goblinPrefab, GameObject goblinArcherPrefab)
    {
        this.goblinPrefab = goblinPrefab;
        this.goblinArcherPrefab = goblinArcherPrefab;
    }
    public int getCost()
    {
        return 1;
    }

    public int getCooldown()
    {
        return 0;
    }

    public override void CopyFrom(AAction source)
    {
        return;
    }

    public override void execute(EntityScript goblin)
    {
        int which = Random.Range(0,2);
        float x = Random.Range(-2.6f, 2.7f);
        float y = Random.Range(-2.4f, 1.6f);
        Vector3 spawnPosition = new Vector3(x, y, 0f);
        if (which == 0)
        {
            Instantiate(goblinPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(goblinArcherPrefab, spawnPosition, Quaternion.identity);
        }
        

        
    }
}
