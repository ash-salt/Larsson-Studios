using System.Linq;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/Melee")]

public class MeleeAttack :  AAction
{
    int damage;
    float spawnDistance = 0.75f;
    GameObject slashInstance;
    public GameObject slashPrefab;
    public Vector3 spawnPos;
    public Quaternion rotation;
    float angle;

    public void Initialize(Quaternion rotation, Vector3 spawnPos)
    {
        this.slashPrefab = GameStateManager.Instance.GetSlashPrefab();
        this.spawnPos = spawnPos;
        this.rotation = rotation;
    }
    public override void CopyFrom(AAction source)
        {
            if (source is MeleeAttack src)  {
                this.slashPrefab = src.slashPrefab;
                this.spawnPos = src.spawnPos;
                this.rotation = src.rotation;
            }
        }

    public void OnEnable()
	{
        buttonInstruction = new MeleeButtonScript(this);
	}

    public ButtonInstruction getInstructions()
        {
            if (buttonInstruction == null) 
                {
                    buttonInstruction = new MeleeButtonScript(this);
                }
            return buttonInstruction;
        }

    public int getCooldown()
    {
        return 0;
    }

    public int getCost()
    {
        return 1;
    }

    public override void execute(EntityScript player)
    {
        slashInstance = GameObject.Instantiate(slashPrefab, spawnPos, rotation);
    }

    public void Dispose()
    {
        Object.Destroy(slashInstance);
    }
}
}