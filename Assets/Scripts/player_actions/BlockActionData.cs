using UnityEngine;
using System.Collections;
namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/BlockAction")]
public class BlockActionData : ActionData
{
    public override AAction createAction()
    {
        return new BlockAction(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return new BlockButtonScript(this);
    }
}
}