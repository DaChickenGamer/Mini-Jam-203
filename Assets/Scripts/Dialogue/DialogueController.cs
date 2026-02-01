using System.Collections;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public AudioSource audioSource;
    
    private bool dialogueActive;
    
    private bool doingTextAnimation;

    private DialogueSequence current;
    private int index;
    private bool waiting;

    public UnityEvent onSequenceEnd;
    
    [SerializeField] private float fadeInDuration = 0.35f;
    [SerializeField] private float fadeOutDuration = 0.25f;


    [SerializeField] private float normalTypeSpeed = 0.05f;
    [SerializeField] private float fastTypeSpeed = 0.005f;
    [SerializeField] private int easedTailLength = 6;

    private float currentTypeSpeed;
    
    EventInstance currentEvent;
    

    public void Play(DialogueSequence sequence)
    {
        current = sequence;
        index = 0;
        ShowLine();
    }

    private void ShowLine()
    {
        dialogueText.text = "";
        
        if (index >= current.lines.Count)
        {
            EndSequence();
            return;
        }

        DialogueLine line = current.lines[index];

        dialogueText.gameObject.SetActive(true);

        dialogueText.DOKill();
        dialogueText.alpha = 0f;

        dialogueText.DOFade(1f, fadeInDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {

                if (!line.voiceOverEvent.IsNull)
                {
                    PlayVoiceOver(line.voiceOverEvent);
                }

                StartCoroutine(DialogueTextAnimation(line.text));
            });
        

        if (line.autoAdvanceDelay > 0)
            StartCoroutine(AutoAdvance(line.autoAdvanceDelay));
    }
    

    private IEnumerator DialogueTextAnimation(string text)
    {
        doingTextAnimation = true;
        waiting = false;
        currentTypeSpeed = normalTypeSpeed;

        dialogueText.text = "";
        dialogueText.alpha = 1f;

        int length = text.Length;

        for (int i = 0; i < length; i++)
        {
            dialogueText.text += text[i];

            bool inTail = i >= length - easedTailLength;
            float delay = inTail
                ? currentTypeSpeed * 1.5f
                : currentTypeSpeed;

            yield return new WaitForSeconds(delay);
        }

        StopVoiceOver();
        doingTextAnimation = false;
        waiting = true;

        dialogueText.alpha = 0.9f;
        dialogueText.DOFade(1f, 0.12f);
    }

    private IEnumerator AutoAdvance(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (waiting)
            Next();
    }

    public void Next()
    {
        if (!waiting) return;

        waiting = false;

        dialogueText.DOKill();
        dialogueText.DOFade(0f, fadeOutDuration)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                dialogueText.gameObject.SetActive(false);
                index++;
                ShowLine();
            });
    }

    public void OnDialogueContinue(InputAction.CallbackContext ctxt)
    {
        if (!ctxt.started) return;

        if (doingTextAnimation) {
            Skip();
            return;
        }
        
        Next();
    }

    private void Skip()
    {
        currentTypeSpeed = fastTypeSpeed;
    }
    
    private void EndSequence()
    {
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(false);

        current = null;
        waiting = false;

        onSequenceEnd?.Invoke();
    }
    
    private void PlayVoiceOver(EventReference eventReference)
    {
        StopVoiceOver();

        currentEvent = RuntimeManager.CreateInstance(eventReference);
        currentEvent.start();
    }
    
    private void StopVoiceOver()
    {
        if (!currentEvent.isValid())
            return;

        currentEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentEvent.release();
        currentEvent.clearHandle();
    }

}
