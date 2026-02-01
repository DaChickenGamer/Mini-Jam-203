using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public AudioSource audioSource;

    private DialogueSequence current;
    private int index;
    private bool waiting;

    public UnityEvent onSequenceEnd;

    public void Play(DialogueSequence sequence)
    {
        current = sequence;
        index = 0;
        ShowLine();
    }

    private void ShowLine()
    {
        if (index >= current.lines.Count)
        {
            EndSequence();
            return;
        }

        var line = current.lines[index];
        dialogueText.text = line.text;

        if (line.voice)
            audioSource.PlayOneShot(line.voice);

        if (line.autoAdvanceDelay > 0)
            Invoke(nameof(Next), line.autoAdvanceDelay);
        else
            waiting = true;
    }

    public void Next()
    {
        waiting = false;
        index++;
        ShowLine();
    }

    public void OnDialogueContinue(InputAction.CallbackContext ctxt)
    {
        if (!ctxt.started || !waiting) return;
        
        Next();
    }
    
    private void EndSequence()
    {
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(false);

        current = null;
        waiting = false;

        onSequenceEnd?.Invoke();
    }

}
