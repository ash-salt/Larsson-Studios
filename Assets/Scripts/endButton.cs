using UnityEngine;

public class endButton : MonoBehaviour
{
    GameStateManager gameStateManager;
    public void Awake()
    {
        gameStateManager = GameStateManager.Instance;
    }
    public void OnMouseDown()
    {
        gameStateManager.startActionPhase();
    }
}