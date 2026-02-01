using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class DialogueLine 
{
    [TextArea(2, 5)]
    public string text;

    public float autoAdvanceDelay;
    public EventReference voiceOverEvent;
}
