using UnityEngine;
using Assets.Scripts.player_actions;
[CreateAssetMenu(menuName = "Scriptable Objects/GoblinRanged")]
public class GoblinRangedData : ActionData
{
    public Sprite attackSprite;
    public Sprite defaultSprite;
    [SerializeField] public GameObject ArrowPrefab;

    public override AAction createAction()
    {
        return new GoblinRangedAttack(this);
    }

    public override ButtonInstruction createButtonInstruction()
    {
        return null;
    }
}