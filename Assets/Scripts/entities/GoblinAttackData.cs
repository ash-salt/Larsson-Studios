using UnityEngine;
using Assets.Scripts.player_actions;
[CreateAssetMenu(menuName = "Scriptable Objects/GoblinMelee")]
public class GoblinAttackData : ActionData
{
    public override AAction createAction()
    {
        return new GoblinAttackAction(this);
    }
    public override ButtonInstruction createButtonInstruction()
    {
        return null;
    }
}