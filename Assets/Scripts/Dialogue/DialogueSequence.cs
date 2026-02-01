using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dialogue/Sequence")]
public class DialogueSequence: ScriptableObject
{
    public List<DialogueLine> lines;
}

