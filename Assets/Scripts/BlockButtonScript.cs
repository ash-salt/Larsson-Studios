using Assets.Scripts.player_actions;
using System.Diagnostics;
using UnityEngine;

public class BlockButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Texture2D cursor;
    [SerializeField] private PlayerScript player;
    [SerializeField] private ActionUIManager actionUIManager;
    [SerializeField] private Sprite actionSprite;
    IAction action = new BlockAction();


    void OnMouseDown()
    {
        print("Clicked!");
        GameStateManager.Instance.newAction(action, actionSprite);
    }
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    
    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    
   


}
