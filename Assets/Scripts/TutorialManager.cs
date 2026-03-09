using Assets.Scripts.player_actions;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public DialogueData[] dialogues;
    private int currentDialogueIndex = 0;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject move;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject melee;
    [SerializeField] private Trap trap;
    [SerializeField] private GameObject goblin;
    private PlayerScript player;
    private int cheat = 0;


    public void Start()
    {
        StartTutorial();
    }
    public void StartTutorial()
    {
        melee.SetActive(false);
        move.SetActive(true);
        block.SetActive(false);
        UI.SetActive(false);
        player = GameStateManager.Instance.player;
        trap.OnTrapTrigger += ShieldDialogue;
        ShowNextDialogue();
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            DialogueData dialogue = dialogues[currentDialogueIndex];
            DialogueManager.Instance.StartDialogue(dialogue);
            currentDialogueIndex++;
        }
        else
        {
            EndTutorial();
        }
    }
    public void MoveDialogue()
    {
        ShowNextDialogue();
    }

    public void ShieldDialogue() {
        ShowNextDialogue();
        ShowNextDialogue();
        trap.OnTrapTrigger -= ShieldDialogue;
        trap.OnTrapTrigger += EnemyEncounter;
        trap.move(new Vector3(1, -2, 0));
        move.SetActive(true);
        block.SetActive(true);

    }

    public void EnemyEncounter() {
        
        trap.OnTrapTrigger -= EnemyEncounter;
        trap.gameObject.SetActive(false);
        //GameStateManager.player
        //Use shenanigans to force a move action to a little further behind
        //add a little delay here
        ShowNextDialogue();
        ShowNextDialogue();

        GameStateManager.Instance.roundFinished += MeleeDialogue;
        player.transform.position = new Vector3(1, -1.2f, -1e-06f);
        goblin.transform.position = new Vector3(1, -2, 0);
        move.SetActive(false);
    }

    //trigger this after a block
    public void MeleeDialogue() {
        cheat++;
        if (cheat < 2) return;
        GameStateManager.Instance.roundFinished -= MeleeDialogue;
        ShowNextDialogue();
        ShowNextDialogue();
        melee.SetActive(true);
        move.SetActive(true);
    }

    private void EndTutorial() {
        ShowNextDialogue();
        //WorldManager.victory();
    }
}