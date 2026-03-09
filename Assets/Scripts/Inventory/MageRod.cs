using Assets.Scripts.player_actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Mage Rod")]
public class MageRod : Item, Characterable
{
    [SerializeField] private ActionData MageAttack;
    [SerializeField] private ActionData MageUtil;
    [SerializeField] private Sprite characterSprite;
    public void Awake()
    {
        description = "Unlocks the power of the mage hidden within.";
    }
    public override void UseItem(PlayerScript player)
    {   
        player.maxHealth -= 25;
        player.currentHealth -= 25;
        player.maxMoveDistance -= 1f;
        player.spriteRenderer.sprite = characterSprite;
        player.attackAction = MageAttack;
        player.utilAction = MageUtil;
    }

    public Sprite GetSprite()
    {
        return characterSprite;
    }
}