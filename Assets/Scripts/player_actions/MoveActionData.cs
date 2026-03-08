using UnityEngine;
using System.Collections;
namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/MoveAction")]
public class MoveActionData : ActionData
{
    public override AAction createAction()
    {
        return new MoveAction(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return new MoveButtonScript(this);
    }
}
}