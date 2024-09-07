using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine 
{
    public string speaker;
    public string text;
    public string spriteName;
}

[Serializable]
public class Dialogue 
{
    public string dialogueName;
    [HideInInspector] public DialogueLine[] lines;
}
