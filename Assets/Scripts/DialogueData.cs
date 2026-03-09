using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue")]
public class DialogueData : ScriptableObject
{
    public Sprite character;
    public string[] lines;
}
