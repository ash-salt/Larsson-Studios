using Assets.Scripts.player_actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/StrongShield")]
public class StrongShield : Item {
    public void Awake()
    {
        description = "Removes the cooldown of your block action";
    }
    public override void UseItem(PlayerScript player)
    {   
        player.utilAction.cooldown = 0;
    }
}