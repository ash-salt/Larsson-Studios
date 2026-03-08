using UnityEngine;
using System.Collections;
namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/MeleeAction")]
public class MeleeAttackData : ActionData
{
    public override AAction createAction()
    {
        return new MeleeAttack(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return new MeleeButtonScript(this);
    }
}
}