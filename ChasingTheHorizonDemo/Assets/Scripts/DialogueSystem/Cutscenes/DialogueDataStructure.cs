using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextLineIndex;
    public string branchId;
}

[Serializable]
public class DialogueLine 
{
    public string branchId;
    public string speaker;
    public string text;
    public string spriteName;
    public DialogueChoice[] choices;
}

[Serializable]
public class Dialogue 
{
    public string dialogueName;
    [HideInInspector] public DialogueLine[] lines;
}
