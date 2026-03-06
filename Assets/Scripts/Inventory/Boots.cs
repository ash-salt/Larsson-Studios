using Assets.Scripts.player_actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Boots")]
public class Boots : Item
{
    [SerializeField] private float speedBoost = 1f;

    public void Awake()
    {
        description = "Increases your movement speed by " + speedBoost;
    }
    public override void UseItem(PlayerScript player)
    {   
        player.maxMoveDistance += speedBoost;
    }
}