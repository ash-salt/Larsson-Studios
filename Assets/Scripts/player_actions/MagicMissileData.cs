using UnityEngine;
using System.Collections;
namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/MagicMissile")]
public class MagicMissileData : ActionData
{
    [SerializeField] public GameObject MagicPrefab;
    public float maxRange = 99f;
    public override AAction createAction()
    {
        return new MagicMissile(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return new MissileScript(this);
    }
}
}