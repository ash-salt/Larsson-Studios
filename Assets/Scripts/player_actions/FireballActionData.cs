using UnityEngine;
using System.Collections;
namespace Assets.Scripts.player_actions {
    [CreateAssetMenu(menuName = "Scriptable Objects/FireballAction")]
public class FireballActionData : ActionData
{
    [SerializeField] public float maxRange;
    public override AAction createAction()
    {
        return new FireballAction(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return new FireballButtonScript(this);
    }
}
}