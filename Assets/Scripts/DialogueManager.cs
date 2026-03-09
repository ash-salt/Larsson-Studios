using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}

    [SerializeField] public GameObject dialougepanel;
    [SerializeField] public TMPro.TextMeshProUGUI dialogueText;
    [SerializeField] public GameObject continueIndicator;
    [SerializeField] public Image speaker;
    [SerializeField] private GameObject UI;
    [SerializeField] private Sprite knight;
    private Queue<DialogueData> queue = new Queue<DialogueData>();

    private string[] lines;
    private int currentLine;
    private bool isTyping;
    private bool activeDialogue;
    private Coroutine typeCoroutine;
    private float charDelay = 0.02f;

    private struct DialogueEntry
{
    public string[] lines;
    public Sprite character;
}

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        dialougepanel.SetActive(false);
        if (continueIndicator) {
            continueIndicator.SetActive(false);
        }
    }
    public void StartDialogue(DialogueData data)
    {

        if (data == null || data.lines.Length == 0) return;

        if (activeDialogue)
        {
            queue.Enqueue(data);
            return;
        }

        PlayDialogue(data);
    } 

    public void OnDialogueClicked()
    {
        if (!activeDialogue) return;

        if (isTyping)
        {
            StopCoroutine(typeCoroutine);
            isTyping = false;
            dialogueText.text = lines[currentLine];
            SetContinueIndicator(true);
        }
        else
        {
            currentLine++;
            if (currentLine < lines.Length)
                ShowLine(currentLine);
            else
                EndDialogue();
        }
    }

    private void PlayDialogue(DialogueData data)
    {
        if (data.character != null)
        {
            speaker.sprite = data.character;
            speaker.gameObject.SetActive(true);
        }
        else
        {
            speaker.sprite = knight;
        }

        this.lines          = data.lines;
        currentLine    = 0;
        activeDialogue = true;

        dialougepanel.SetActive(true);
        BlockGameInput(true);
        ShowLine(0);
    }
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(charDelay); 
        }
        isTyping = false;
        SetContinueIndicator(true);
    }

    private void ShowLine(int index)
    {
        SetContinueIndicator(false);
        dialogueText.text = string.Empty;
        typeCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    private void EndDialogue()
    {
        activeDialogue = false;
        if (queue.Count > 0)
        {
            StartDialogue(queue.Dequeue());
            return;
        }
        dialougepanel.SetActive(false);
        BlockGameInput(false);
        NotifyDialogue();
        if (continueIndicator) continueIndicator.SetActive(false);
    }

    public event Action queueFinished;
	public void NotifyDialogue()
	{		
			queueFinished?.Invoke();
	}

    private void SetContinueIndicator(bool visible)
    {
        if (continueIndicator) continueIndicator.SetActive(visible);
    }

    private void BlockGameInput (bool block) {
        UI.SetActive(!block);
    }
}