using UnityEngine;
using Assets.Scripts.player_actions;
using TMPro;

public class HealthBarControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is creat
    public PlayerScript PlayerControl;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        HealthChanged();
    }


    public void HealthChanged()
    {
        healthText.text = $"{PlayerControl.currentHealth}/{PlayerControl.maxHealth}";
    }
}

