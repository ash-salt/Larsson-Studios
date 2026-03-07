using Assets.Scripts.player_actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Mage Rod")]
public class MageRod : Item
{
    [SerializeField] private AAction MageAttack;
    [SerializeField] private AAction MageUtil;

    public void Awake()
    {
        description = "Unlocks the power of the mage hidden within.";
    }
    public override void UseItem(PlayerScript player)
    {   
        player.maxHealth -= 25;
        player.maxMoveDistance -= 1f;
        player.spriteRenderer.color = new Color(0.5f, 0.5f, 1f);
        //player.attackAction = MageAttack;
        //player.utilAction = MageUtil;
    }
}