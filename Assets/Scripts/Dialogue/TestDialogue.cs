using System;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    public DialogueSequence testSequence;
    public DialogueController dialogueController;
    private void Start()
    {
       dialogueController.Play(testSequence); 
    }
}
