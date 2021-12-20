using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "MapDialogue")]
public class MapDialogue : ScriptableObject
{
    public bool textBox1;
    public bool textBox2;
    public bool battleDialogue;
    public bool scriptDialogue;    
    public Unit unit;
    [TextArea(2, 5)]
    public string dialogue;
    [HideInInspector]
    public bool finished;
}
