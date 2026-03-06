using Assets.Scripts.player_actions;
using UnityEngine;
using UnityEngine.UI;   // ✅ This must work now

public class UndoButtonVisibleScript : MonoBehaviour
{
    private PlayerScript player;

    [SerializeField] private Button button;

    void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
    }

    void Update()
    {
        if (player == null || button == null) return;

        bool hasActions = player.actions.Count > 0;

        button.gameObject.SetActive(hasActions);
        // or: button.interactable = hasActions;
    }
}
