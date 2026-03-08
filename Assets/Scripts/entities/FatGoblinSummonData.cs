using UnityEngine;
using Assets.Scripts.player_actions;
[CreateAssetMenu(menuName = "Scriptable Objects/SummonAction")]
public class FatGoblinSummonData : ActionData
{
    [SerializeField] public GameObject goblinPrefab;
    [SerializeField] public GameObject goblinArcherPrefab;

    public override AAction createAction()
    {
        return new FatGoblinSummonAction(this);
    }
    public override ButtonInstruction createButtonInstruction()
    {
        return null;
    }
}