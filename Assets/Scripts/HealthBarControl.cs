using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts.player_actions;

public class HealthBarControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is creat
    public PlayerScript PlayerControl;
    public UIDocument UIDoc;

    private Label m_HealthLabel;

    private void Start()
    {
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("Healthlabel");

        HealthChanged();
    }


    public void HealthChanged()
    {
        m_HealthLabel.text = $"{PlayerControl.currentHealth}/{PlayerControl.maxHealth}";
    }
}

